using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Настройки движения (Редактируются в Инспекторе)
    public float speed = 5.0f; // Скорость движения
    public float turnSpeed = 5.0f; // Скорость вращения

    // Система ввода (редактируется в Инспекторе)
    public InputAction MoveAction;
    public InputAction switchCameraAction; // Смена вида камеры

    // Ссылки на камеры (основная и вид от капота)
    public GameObject mainCamera; 
    public GameObject hoodCamera;

    // Текущее значение ввода (x - право/лево, y - вперёд/назад)
    private Vector2 moveInput; // Вектор движения

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MoveAction.Enable(); // Активируем MoveAction для считывания ввода
        switchCameraAction.Enable(); // Активируем считывания клавиши смены вида камеры

        // Проверка, указаны ли камеры и активация основной камеры по умолчанию
        if (mainCamera != null && hoodCamera != null)
        {
            mainCamera.SetActive(true);
            hoodCamera.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Считываем 2D вектор из MoveAction (x: вертикальный, y: горизонтальный)
        moveInput = MoveAction.ReadValue<Vector2>();

        // Двигаем транспорт вперёд или назад вдоль оси Z, используя y компонент
        transform.Translate(Vector3.forward * Time.deltaTime * speed * moveInput.y);

        // Вращаем транспорт вокруг оси y, используя x компонент
        if(moveInput.y > 0)
            transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed * moveInput.x); // Если движемся вперёд
        else if (moveInput.y < 0)
            transform.Rotate(Vector3.down * Time.deltaTime * turnSpeed * moveInput.x); // Если движемся назад

        // Проверка нажатия кнопки (Срабатывает 1 раз за клик)
        if (switchCameraAction.WasPressedThisFrame())
        {
            // Переключаем активную камеру
            bool isMainActive = mainCamera.activeSelf; // Проверяем, какая камера в данный момент активна
            mainCamera.SetActive(!isMainActive); // Если активна не основная, то активируем её
            hoodCamera.SetActive(isMainActive); // Если активна основная, то активируем камеру от капота
        }
    }

    // Отключаем управление, если достигли финиша
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            MoveAction.Disable(); // Деактивируем MoveAction
            switchCameraAction.Disable(); // Заодно отключаем и смену камеры
        }
    }
}

using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player; // Наш объект игрок
    public Vector3 offset = new Vector3(0, 6, -8); // Отступ камеры от позиции игрока

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Фиксируем камеру за игроком
        transform.position = player.transform.position + offset;
    }
}

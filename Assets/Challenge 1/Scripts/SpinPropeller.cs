using System.Runtime.CompilerServices;
using UnityEngine;

public class SpinPropeller : MonoBehaviour
{

    public float spinSpeed = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, spinSpeed);
    }
}

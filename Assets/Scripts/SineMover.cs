using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineMover : MonoBehaviour
{
    [SerializeField]
    float moveSpeed; // Vertical movement speed

    [SerializeField]
    float frequency; // How often object goes left and right

    [SerializeField]
    float magnitude; // The highest/lowest points the object will go in the wave

    Vector3 pos;
    
    void Start()
    {
        pos = transform.position;
    }

    void Update()
    {
        pos -= transform.up * Time.deltaTime * moveSpeed;
        transform.position = pos + transform.right * Mathf.Sin(Time.time * frequency) * magnitude;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObject : MonoBehaviour
{
    public bool isRotatingX;
    public bool isRotatingY;
    public bool isRotatingZ;

    // Changeable value for different objects;
    public float rotateSpeed;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Rotates in a forwards and backwards circle motion
        if (isRotatingX)
        {
           transform.Rotate (new Vector3 (1* rotateSpeed, 0, 0) * Time.deltaTime);
        }
        
        // Rotates in a flat circle motion
        if (isRotatingY)
        {
           transform.Rotate (new Vector3 (0, 1* rotateSpeed, 0) * Time.deltaTime);
        }

        // Rotates in a left to right circle motion
        if (isRotatingZ)
        {
           transform.Rotate (new Vector3 (0, 0, 1* rotateSpeed) * Time.deltaTime);
        }

        
    }
}

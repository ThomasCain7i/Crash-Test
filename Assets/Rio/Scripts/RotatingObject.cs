using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObject : MonoBehaviour
{
    // Bools to rotate in different directions
    
    public bool isRotatingX1;
    public bool isRotatingX2;

    public bool isRotatingY1;
    public bool isRotatingY2;

    public bool isRotatingZ1;
    public bool isRotatingZ2;

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
        if (isRotatingX1)
        {
           transform.Rotate (new Vector3 (1* rotateSpeed, 0, 0) * Time.deltaTime);
        }

        if (isRotatingX2)
        {
           transform.Rotate (new Vector3 (-1* rotateSpeed, 0, 0) * Time.deltaTime);
        }
        
        // Rotates in a flat circle motion
        if (isRotatingY1)
        {
           transform.Rotate (new Vector3 (0, 1* rotateSpeed, 0) * Time.deltaTime);
        }

        if (isRotatingY2)
        {
           transform.Rotate (new Vector3 (0, -1* rotateSpeed, 0) * Time.deltaTime);
        }

        // Rotates in a left and right circle motion
        if (isRotatingZ1)
        {
           transform.Rotate (new Vector3 (0, 0, 1* rotateSpeed) * Time.deltaTime);
        }

        if (isRotatingZ2)
        {
           transform.Rotate (new Vector3 (0, 0, -1* rotateSpeed) * Time.deltaTime);
        }

    }
}

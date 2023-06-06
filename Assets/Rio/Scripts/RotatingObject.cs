using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObject : MonoBehaviour
{
    public bool isRotatingX;
    public bool isRotatingY;
    public bool isRotatingZ;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isRotatingX)
        {
           transform.Rotate (new Vector3 (20, 0, 0) * Time.deltaTime);
        }
        
        if (isRotatingY)
        {
           transform.Rotate (new Vector3 (0, 20, 0) * Time.deltaTime);
        }

        if (isRotatingZ)
        {
           transform.Rotate (new Vector3 (0, 0, 20) * Time.deltaTime);
        }

        
    }
}

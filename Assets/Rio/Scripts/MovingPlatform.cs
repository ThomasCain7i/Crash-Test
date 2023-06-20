// Rio

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float moveTimer;

    //Moving left and right - Rio
    public bool isMovingLR;

    //Moving forwards and backwards - Rio
    public bool isMovingFB;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveTimer += Time.deltaTime;

        if (isMovingLR)
        {
            if (moveTimer <= 5.0f)
            {
            //Platform moves right - Rio
            transform.Translate(new Vector3(1 * Time.deltaTime, 0, 0));

            }
            else if (moveTimer <= 10.0f)
            {
            //Platform moves left- Rio
            transform.Translate(new Vector3(-1 * Time.deltaTime, 0, 0));

            }
            else if (moveTimer >= 10.0f)
            {
            // resets timer - Rio
            moveTimer = 0;
            }
        }

        if (isMovingFB)
        {
            if (moveTimer <= 5.0f)
            {
            //Platform moves forwards - Rio
            transform.Translate(new Vector3(0, 0, 1 * Time.deltaTime));

            }
            else if (moveTimer <= 10.0f)
            {
            //Platform moves backwards - Rio
            transform.Translate(new Vector3(0, 0, -1 * Time.deltaTime));

            }
            else if (moveTimer >= 10.0f)
            {
            // resets timer - Rio
            moveTimer = 0;
            }
        }
    }
}

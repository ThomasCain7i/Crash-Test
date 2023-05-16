using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float moveTimer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveTimer += Time.deltaTime;

        if (moveTimer <= 5.0f)
        {
            //Platform moves right
            transform.Translate(new Vector3(1 * Time.deltaTime, 0, 0));

        }
        else if (moveTimer <= 10.0f)
        {
            //Platform moves left
            transform.Translate(new Vector3(-1 * Time.deltaTime, 0, 0));

        }
        else if (moveTimer >= 10.0f)
        {
            // resets timer
            moveTimer = 0;
        }
    }
}

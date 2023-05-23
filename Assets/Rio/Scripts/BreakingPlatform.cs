using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingPlatform : MonoBehaviour
{
    public float fallSpeed;
    public float fallTimer;

    public Material on, off;
    Renderer r;

    public GameObject player;

    public bool isOn;
    public bool isOff;

    // Start is called before the first frame update
    void Start()
    {
        fallSpeed = 2.0f;

        r = GetComponent<Renderer>();

       isOff = true;
    }

    // Update is called once per frame
    void Update()
    {
       if (isOff == true)
       {
            r.material = off;  
       }
       else if (isOn == true)
       {
            fallTimer += Time.deltaTime;
            if (fallTimer >= 0.5)
            {
                r.material = on;
                //Platform falls down
                transform.Translate(new Vector3(0, -1 * fallSpeed *Time.deltaTime, 0));


            }
       }



        if (fallTimer >= 1.5f)
        {

            Destroy(gameObject);
        }

    }

    private void OnCollisionStay(Collision collision)
    {
       
        if (collision.gameObject.tag == "Player")
        {
            isOn = true;
            
            isOff = false;
            r.material = on;

        }

    }

    private void OnCollisionExit(Collision collision)
    {
        // Stops from descending
        if (fallTimer <= 0.5)
        {
          isOn = false;
          isOff = true;
          fallTimer = 0;
        }
         // Will descend and break after a small period when the player steps on the platform
        else if (fallTimer >= 0.5)
        {
            isOn = true;
            isOff = false;
           
        }
        
    }
}

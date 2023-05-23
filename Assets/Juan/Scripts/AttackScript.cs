using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    //BARK STUFF (POINT OF SPAWN, SPEED ETC..)
    public Transform barkSpawn;
    public GameObject barkBullet;
    public float barkSpeed = 5f;

    //COOLDOWN
    public float coolDownTime;
    public float nextBarkTime;

    // Rigidbody
    private Rigidbody rb;
    private bool isGrounded;

    //SMASH
    public float smashForce; 
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextBarkTime)
        {
            //BARK ATTACK - JUAN
            if (Input.GetKeyDown(KeyCode.R))
            {
                var ability = Instantiate(barkBullet, barkSpawn.position, barkSpawn.rotation); //SPAWNS THE ATTACK FROM THE SPECIFIC POINT OFF THE PLAYER
                ability.GetComponent<Rigidbody>().velocity = barkSpawn.forward * barkSpeed; //DEALS WITH THE FORCE AND SPEED OF THE BARK


                //COOLDOWN FOR THE BARK - JUAN
                nextBarkTime = Time.time + coolDownTime;
            }
        }
       
        //SMASH ATTACK - JUAN

        if(isGrounded == false)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                rb.AddForce(Vector3.down * smashForce, ForceMode.VelocityChange);
            }
        }
    }
               


    
}

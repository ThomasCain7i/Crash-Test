using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public Transform barkSpawn;
    public GameObject barkBullet;
    public float barkSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            var ability = Instantiate(barkBullet, barkSpawn.position, barkSpawn.rotation);
            ability.GetComponent<Rigidbody>().velocity = barkSpawn.forward * barkSpeed;
           
        }
    }
}

          





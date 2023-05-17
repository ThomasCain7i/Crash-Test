using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    public PlayerController playerController;
    

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }



    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            Debug.Log("Ranged Attack");

            // Player shoots projectile during spin attack
            
            /*gets player attack controls
             * instantiates game object projectile
             * projectile deals damage
            */

        }
    }
}

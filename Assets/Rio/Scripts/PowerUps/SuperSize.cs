// Rio 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperSize : MonoBehaviour
{
   public PlayerControllerRIO playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerControllerRIO>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            Debug.Log("Size boost");

            // Player becomes larger - Rio
            PlayerControllerRIO thePlayer = other.gameObject.GetComponent<PlayerControllerRIO>();

            //collects powerup by setting the players bool to true - Rio
            thePlayer.MegaPowerUpFunction();

            Destroy(gameObject);
        }
    }
}

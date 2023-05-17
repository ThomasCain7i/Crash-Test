using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperSpeed : MonoBehaviour
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
            Debug.Log("Speed Boost");

            // Player becomes faster
            PlayerController thePlayer = other.gameObject.GetComponent<PlayerController>();

            //collects powerup by setting the players bool to true
            thePlayer.SpeedPowerUpFunction();

            Destroy(gameObject);
        }
    }
}

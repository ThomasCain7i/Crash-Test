// Rio

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarPower : MonoBehaviour
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
            Debug.Log("Invincible");

            // Player becomes invincible - Rio
            PlayerControllerRIO thePlayer = other.gameObject.GetComponent<PlayerControllerRIO>();

            //collects powerup by setting the players bool to true - Rio
            thePlayer.StarPowerUpFunction();

            Destroy(gameObject);
        }
    }
}

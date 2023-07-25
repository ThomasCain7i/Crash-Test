using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUpC : MonoBehaviour
{
    private PowerUpManager powerUpManager;  // Reference to the PowerUpManager script
    public PlayerControllerCam playerController;

    // Start is called before the first frame update
    void Start()
    {
        powerUpManager = FindObjectOfType<PowerUpManager>();  // Find and assign the PowerUpManager component in the scene
        playerController = FindObjectOfType<PlayerControllerCam>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            Debug.Log("Speed Boost");

            powerUpManager.DeactivatePowerUp(gameObject);  // Call the DeactivatePowerUp method from the PowerUpManager, passing the current game object

            //collects powerup by setting the players bool to true
            playerController.SpeedPowerUp();
        }
    }
}

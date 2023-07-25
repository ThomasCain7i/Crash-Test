using UnityEngine;

public class TimeSlowC : MonoBehaviour
{
    private PowerUpManager powerUpManager;  // Reference to the PowerUpManager script
    public PlayerControllerCam playerController;  // Reference to the PlayerController script

    void Start()
    {
        powerUpManager = FindObjectOfType<PowerUpManager>();  // Find and assign the PowerUpManager component in the scene
        playerController = FindObjectOfType<PlayerControllerCam>();  // Find and assign the PlayerController component in the scene
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            powerUpManager.DeactivatePowerUp(gameObject);  // Call the DeactivatePowerUp method from the PowerUpManager, passing the current game object

            playerController.TimeSlowPowerUp();  // Call the TripleJump method from the PlayerController
        }
    }
}
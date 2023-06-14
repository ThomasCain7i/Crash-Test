using UnityEngine;

public class Armour : MonoBehaviour
{
    private UIManager uiManager;  // Reference to the PowerUpManager script
    public PlayerController playerController;  // Reference to the PlayerController script

    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();  // Find and assign the PowerUpManager component in the scene
        playerController = FindObjectOfType<PlayerController>();  // Find and assign the PlayerController component in the scene
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (playerController.Armour == 0)
            {
                playerController.Armour = 1;
                uiManager.ArmourUIon();

                Destroy(gameObject); // If the current health drops to or below zero, destroy the enemy object
            }
        }
    }
}
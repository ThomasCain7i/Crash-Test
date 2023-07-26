using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public PlayerController playerController; // Reference to the PlayerController script
    public PlayerControllerCam pc; // Reference to the PlayerControllerCam script

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>(); // Find and assign the PlayerController component in the scene
        pc = FindObjectOfType<PlayerControllerCam>(); // Find and assign the PlayerControllerCam component in the scene
    }

    // Called when a collider enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") // Check if the collider has the "Player" tag
        {
            playerController.SetSpawnPoint(transform.position); // Call the SetSpawnPoint method in PlayerController and pass the current checkpoint's position
            pc.SetSpawnPoint(transform.position); // Call the SetSpawnPoint method in PlayerController and pass the current checkpoint's position
        }
    }
}
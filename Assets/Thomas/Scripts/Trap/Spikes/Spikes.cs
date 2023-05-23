using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    // Start is called before the first frame update
    public int damage;
    public PlayerController playerController;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player hit by spikes");
        if (other.tag == "Player")
        {
            if (playerController != null)
            {
                playerController.currentHealth -= damage;
            }
        }
    }
}
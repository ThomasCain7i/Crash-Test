using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    public PlayerController playerController; // Reference to the PlayerController script
    public float damage = 3; // Damage caused by the spikes
    private float destroyTimer = 0.25f; // Timer for destroying the spikes

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>(); // Find and assign the PlayerController component in the scene
    }

    private void Update()
    {
        Destroy(gameObject, destroyTimer); // Destroy the spikes after the destroyTimer duration
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") // Check if the collider's tag is "Player"
        {
            Debug.Log("Player touched the spikes");
            other.gameObject.GetComponent<PlayerController>().TakeDamage(damage); // Call the TakeDamage method of the PlayerController component attached to the player
            Destroy(gameObject); // Destroy the spikes
        }
    }
}
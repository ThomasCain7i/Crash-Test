using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float currentHealth; // The current health of the enemy
    public float maxHealth; // The maximum health of the enemy

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth; // Initialize the current health to the maximum health
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount; // Subtract the damage amount from the current health

        if (currentHealth <= 0)
        {
            Destroy(gameObject); // If the current health drops to or below zero, destroy the enemy object
        }
    }
}
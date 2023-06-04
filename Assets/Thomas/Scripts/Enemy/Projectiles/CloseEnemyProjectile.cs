using UnityEngine;

public class CloseEnemyProjectile : MonoBehaviour
{
    public CloseEnemy closeEnemy; // Reference to the CloseEnemy script
    public float damage; // Damage caused by the projectile

    // Start is called before the first frame update
    void Start()
    {
        closeEnemy = FindObjectOfType<CloseEnemy>(); // Find and assign the CloseEnemy component in the scene
        damage = closeEnemy.damage; // Set the damage value from the CloseEnemy component
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") // Check if the collider's tag is "Player"
        {
            Debug.Log("Player touched the close enemy projectile");
            other.gameObject.GetComponent<PlayerController>().TakeDamage(damage); // Call the TakeDamage method of the PlayerController component attached to the player
        }
    }
}
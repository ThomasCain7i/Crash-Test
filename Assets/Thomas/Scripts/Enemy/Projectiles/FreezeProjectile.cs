using UnityEngine;

public class FreezeProjectile : MonoBehaviour
{
    public float destroyTimer = 1; // Timer for destroying the projectile
    public RangedEnemy rangedEnemy; // Reference to the RangedEnemy script
    public float damage; // Damage caused by the projectile

    // Start is called before the first frame update
    void Start()
    {
        rangedEnemy = FindObjectOfType<RangedEnemy>(); // Find and assign the RangedEnemy component in the scene
        damage = rangedEnemy.damage; // Set the damage value from the RangedEnemy component
    }

    private void Awake()
    {
        Destroy(gameObject, destroyTimer); // Destroy the projectile after the destroyTimer duration
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") // Check if the collider's tag is "Player"
        {
            Debug.Log("Player touched the enemy projectile");
            other.gameObject.GetComponent<PlayerController>().Frozen(); // Call the Frozen method of the PlayerController component attached to the player
            Destroy(gameObject); // Destroy the projectile
        }
    }
}
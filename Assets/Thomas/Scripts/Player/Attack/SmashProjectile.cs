using UnityEngine;

public class SmashProjectile : MonoBehaviour
{
    public float damage;
    public float destroyTimer = .5f;
    public PlayerController playerController;
    public float knockbackForce = 10f; // Adjust this value to control the strength of the knockback force

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();  // Find and assign the PlayerController component in the scene
        damage = playerController.smashDamage;
    }

    private void Update()
    {
        Destroy(gameObject, destroyTimer);
    }

    private void OnTriggerEnter(Collider other)
    {
        //On collision with bullet do:
        switch (other.gameObject.tag)
        {
            // Collision with enemy deal damage and apply knockback
            case "Enemy":
                EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
                enemyHealth.TakeDamage(damage);

                Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
                if (enemyRigidbody != null)
                {
                    Vector3 knockbackDirection = other.transform.position - transform.position;
                    knockbackDirection.Normalize();
                    enemyRigidbody.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
                }
                break;
        }
    }
}
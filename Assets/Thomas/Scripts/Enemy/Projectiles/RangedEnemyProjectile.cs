using UnityEngine;

public class RangedEnemyProjectile : MonoBehaviour
{
    public float destroyTimer = 1;
    public RangedEnemy rangedEnemy;
    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        rangedEnemy = FindObjectOfType<RangedEnemy>();  // Find and assign the PlayerController component in the scene
        damage = rangedEnemy.damage;
    }

    private void Awake()
    {
        Destroy(gameObject, destroyTimer);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player touched the enemy projectile");
            other.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
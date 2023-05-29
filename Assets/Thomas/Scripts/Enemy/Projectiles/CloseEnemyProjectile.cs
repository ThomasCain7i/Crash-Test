using UnityEngine;

public class CloseEnemyProjectile : MonoBehaviour
{

    public CloseEnemy closeEnemy;
    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        closeEnemy = FindObjectOfType<CloseEnemy>();  // Find and assign the PlayerController component in the scene
        damage = closeEnemy.damage;
    }

    private void Awake()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player touched the close enemy projectile");
            other.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
        }
    }
}
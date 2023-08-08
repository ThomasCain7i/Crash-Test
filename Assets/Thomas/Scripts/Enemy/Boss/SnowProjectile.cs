using UnityEngine;

public class SnowProjectile : MonoBehaviour
{
    public float speed, timer = 1f, destroyTimer; // The speed at which the projectile moves
    [SerializeField]
    private Boss boss;

    private void Start()
    {
        boss = FindObjectOfType<Boss>();
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        destroyTimer -= Time.deltaTime;

        speed = boss.projectileSpeed;

        if (timer < 0)
        {
            // Move the projectile forward
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        }

        if (destroyTimer <= 0)
        {
            Destroy(gameObject);
        }
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
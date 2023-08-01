using UnityEngine;

public class FireBarkScript : MonoBehaviour
{
    public float damage;
    public float destroyTimer = 1;
    public PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();  // Find and assign the PlayerController component in the scene
        damage = playerController.fireBarkDamage;
    }

    private void Awake()
    {
        Destroy(gameObject, destroyTimer);
    }

    private void OnTriggerEnter(Collider other)
    {
        //On collision with bullet do:
        switch (other.gameObject.tag)
        {
            // Collision with wall just destroy bullet
            case "Wall":
                Destroy(other.gameObject);
                Destroy(gameObject);
                break;
            // Collision with enemy deal 1 damage and destory
            case "Enemy":
                other.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
                Destroy(gameObject);
                break;
        }
    }
}
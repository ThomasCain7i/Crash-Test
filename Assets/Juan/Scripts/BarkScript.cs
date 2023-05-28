using UnityEngine;

public class BarkScript : MonoBehaviour
{
    public float destroyTimer = 1;
    public PlayerController playerController;
    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();  // Find and assign the PlayerController component in the scene
        damage = playerController.barkDamage;
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
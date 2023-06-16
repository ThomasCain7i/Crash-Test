using UnityEngine;

public class SmashProjectile : MonoBehaviour
{
    public float damage;
    public float destroyTimer = .5f;
    public PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();  // Find and assign the PlayerController component in the scene
        damage = playerController.smashDamage;
    }

    private void Update()
    {
        destroyTimer -= Time.deltaTime;

        if ( destroyTimer < 0 )
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //On collision with bullet do:
        switch (other.gameObject.tag)
        {
            // Collision with enemy deal 1 damage and destory
            case "Enemy":
                other.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
                break;
        }
    }
}
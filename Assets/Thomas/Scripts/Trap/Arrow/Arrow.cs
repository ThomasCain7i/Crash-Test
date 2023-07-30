using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float damage = 1;
    public float timer = 5;
    public PlayerController playerController;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        audioSource = GetComponent<AudioSource>();

        audioSource.pitch = Random.Range(0.8f, 1.2f);
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
         //On collision with bullet do:
         switch (other.gameObject.tag)
         {
             // Collision with wall just destroy bullet
             case "UnbreakableWall":
                 Destroy(gameObject);
                 break;
             // Collision with player deal 1 damage and destory
             case "Player":
                 Debug.Log("Player touched the arrow");
                 other.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
                 Destroy(gameObject);
                 break;
         }
    }
}

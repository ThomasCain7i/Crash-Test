using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float damage = 1;
    private float destoryTime = 5;
    public PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        destoryTime -= Time.deltaTime;

        if (destoryTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player touched the arrow");
            other.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}

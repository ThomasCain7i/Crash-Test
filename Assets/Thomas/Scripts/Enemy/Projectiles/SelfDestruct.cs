using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    public PlayerController playerController;
    public float damage = 3;
    private float destroyTimer = .25f;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        Destroy(gameObject, destroyTimer);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player touched the spikes");
            other.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}

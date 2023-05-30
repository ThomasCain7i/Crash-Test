using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    public PlayerController playerController;
    public float damage = 3;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
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

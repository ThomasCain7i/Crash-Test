using UnityEngine;

public class Boulder : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private BoulderTrigger trigger;
    [SerializeField]
    private float damage = 5;
    [SerializeField]
    private Transform startPos;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        trigger = FindObjectOfType<BoulderTrigger>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the wall collides with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player touched the boulder");
            playerController.Armour = 0;
            playerController.TakeDamage(damage);
            trigger.floor.SetActive(true);
            transform.position = startPos.position;
        }
    }
}
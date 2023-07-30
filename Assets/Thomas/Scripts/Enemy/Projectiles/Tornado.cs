using UnityEngine;

public class Tornado : MonoBehaviour
{
    public float damage; // Damage caused by the projectile

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") // Check if the collider's tag is "Player"
        {
            Debug.Log("Player touched the tornado");
            other.gameObject.GetComponent<PlayerController>().TakeDamage(damage); // Call the TakeDamage method of the PlayerController component attached to the player
        }
    }
}
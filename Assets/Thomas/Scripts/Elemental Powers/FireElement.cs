using UnityEngine;

public class FireElement : MonoBehaviour
{
    public AttackScript attackScript;  // Reference to the PlayerController script

    void Start()
    {
        attackScript = FindObjectOfType<AttackScript>();  // Find and assign the PlayerController component in the scene
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            attackScript.fire = true;  // Set fire bool to true
            Destroy(gameObject);
        }
    }
}
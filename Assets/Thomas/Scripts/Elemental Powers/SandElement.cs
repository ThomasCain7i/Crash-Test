// Thomas

using UnityEngine;

public class SandElement : MonoBehaviour
{
    public AttackScript attackScript;  // Reference to the PlayerController script
    public GameManager gameManager;

    void Start()
    {
        attackScript = FindObjectOfType<AttackScript>();  // Find and assign the PlayerController component in the scene
        gameManager = FindObjectOfType<GameManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            attackScript.sand = 1;  // Set fire bool to true
            gameManager.SaveElements();
            Destroy(gameObject);
        }
    }
}
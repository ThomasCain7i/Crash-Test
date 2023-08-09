// Thomas

using UnityEngine;

public class FireElement : MonoBehaviour
{
    public AttackScript attackScript;  // Reference to the PlayerController script
    public GameManager gameManager;

    [SerializeField]
    private float speed;

    void Start()
    {
        attackScript = FindObjectOfType<AttackScript>();  // Find and assign the PlayerController component in the scene
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        transform.Rotate(0f, speed * Time.deltaTime, 0f, Space.Self);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            attackScript.fire = 1;  // Set fire bool to true
            Destroy(gameObject);
        }
    }
}
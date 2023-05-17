using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Health / lives / bones
    public int lives = 3;
    public int maxHealth = 3;
    public int currentHealth = 3;
    public int boneCount = 0;
    
    // Movement
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public int maxJumps = 2;
    private int jumpsRemaining;

    // Rigidbody
    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jumpsRemaining = maxJumps;
        currentHealth = maxHealth;
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical) * moveSpeed;
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);

        if (Input.GetButtonDown("Jump") && (isGrounded || jumpsRemaining > 0))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            isGrounded = false;
            jumpsRemaining--;
        }

        // Turn the player depending on how they move
        if (moveHorizontal < 0)
        {
            transform.rotation = Quaternion.Euler(0f, 270f, 0f);
        }
        else if (moveHorizontal > 0)
        {
            transform.rotation = Quaternion.Euler(0f, 90f, 0f);
        }
        else if (moveVertical < 0)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else if (moveVertical > 0)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }

        if (currentHealth <= 0)
        {
            //Respawn at previous checkpoint

            lives -= 1;

            if (lives <= 0)
            {
                //End game
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpsRemaining = maxJumps;
        }
    }

    public void GainHealth()
    {
        if (currentHealth < maxHealth)
        {
            int health = 1;
            currentHealth = currentHealth + health;
        }
      
    }

    public void Collectedbone()
    {
        boneCount = boneCount + 1;
        //rough idea
        if (boneCount == 10)
        {
            // powerup function
       
        }

    }
}
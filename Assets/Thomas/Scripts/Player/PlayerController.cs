using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Health / lives / bones
    public int lives = 3;
    public int maxHealth = 3;
    public int currentHealth = 3;
    public bool isDead = false;
    public float deadTimer;
    public int boneCount = 0;
    
    // Movement
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public int maxJumps = 2;
    private int jumpsRemaining;

    // Rigidbody
    private Rigidbody rb;
    private bool isGrounded;

    // Powerups
    public bool speedPowerup;
    public float speedTimer;
    public float maxSpeedTimer;
    public bool rangedPowerup;

    // Respawn point
    public Vector3 respawnPoint;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jumpsRemaining = maxJumps;
        currentHealth = maxHealth;
    }

    void Update()
    {
        // Input system movement - Thomas
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical) * moveSpeed;
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);

        // Jump with ground checker - Thomas
        if (Input.GetButtonDown("Jump") && (isGrounded || jumpsRemaining > 0))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            isGrounded = false;
            jumpsRemaining--;
        }

        // Turn the player depending on how they move - Thomas
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

        // If health = 0 - Thomas
        if (currentHealth <= 0)
        {
            //Respawn at previous checkpoint
            isDead = true;
            lives -= 1;

            if (lives <= 0)
            {
                //End game
            }
        }

        SuperSpeed();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            //Reset jumps and set grounded true - Thomas
            isGrounded = true;
            jumpsRemaining = maxJumps;
        }
    }

    //When picking up dog treat use GainHealth to increase health by 1 - Thomas
    public void GainHealth()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth = currentHealth + 1;
        }
      
    }

    public void Respawn()
    {
        if(isDead == true)
        {
            deadTimer = 5f;
            deadTimer -= Time.deltaTime;

            if (deadTimer < 0)
            {
                //transform.position = 
            }
        }
    }

    public void SetSpawnPoint(Vector3 newPosition)
    {
        respawnPoint = newPosition;
    }

    // When picking up bones use this function - Thomas
    public void Collectedbone()
    {
        boneCount = boneCount + 1;
        //rough idea
        if (boneCount == 10)
        {
            // powerup function
       
        }

    }

    // Speed powerup
    public void SpeedPowerUpFunction()
    {
        // Activates super speed
        speedPowerup = true;

        // resets timer
        speedTimer = maxSpeedTimer;
    }

    public void SuperSpeed()
    {
        if(speedPowerup == true)
        {
            Debug.Log("Speed Boost");

            // Player becomes faster
            moveSpeed = 7.5f;
        }
        else
        {
            // returns to default speed
            moveSpeed = 5f;
        }

        speedTimer += Time.deltaTime;
        if (speedTimer >= 20.0f)
        {
            // Power works for a limited time
            speedPowerup = false;

        }

    }
}
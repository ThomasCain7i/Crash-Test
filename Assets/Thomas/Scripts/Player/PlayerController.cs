using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Health / lives
    public int lives = 3;
    public int maxHealth = 3;
    public int currentHealth = 3;

    // Bones
    public int boneCount = 0;
    
    // Movement
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public int maxJumps = 2;
    private int jumpsRemaining;

    // Rigidbody / Ground test
    private Rigidbody rb;
    private bool isGrounded;

    // Powerups
    public float speedTimer;
    public float tripleJumpTimer;

    // Debuffs
    public bool isSlowed = false;

    // Respawn point
    public Vector3 respawnPoint;

    // Platforms
    private BreakingPlatform breakingPlatform;

    void Start()
    {
        //Get rigid body of player
        rb = GetComponent<Rigidbody>();

        //Set jumps and health to max
        jumpsRemaining = maxJumps;
        currentHealth = maxHealth;
    }

    void Update()
    {
        // MOVEMENT
        // Input system movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical) * moveSpeed;
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);

        // Jump with ground checker
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

        // HEALTH / LIVES
        // If health = 0
        if (lives >= 1)
        {
            if (currentHealth <= 0)
            {
                //Respawn
                Respawn();
            }
        }
        else
        {
            //end game
            Debug.Log("Lives = 0");
        }
 
        // POWER UPS
        // Triple jump powerup
        tripleJumpTimer -= Time.deltaTime;
        speedTimer -= Time.deltaTime;

        if(tripleJumpTimer < 0)
        {
            maxJumps = 2;
        }  

        if(speedTimer < 0 && !isSlowed)
        {
            moveSpeed = 5;
        }
    }

    // Collision stuff
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            //Reset jumps and set grounded true
            isGrounded = true;
            jumpsRemaining = maxJumps;
        }
    }
   
    // GAINING HEALTH METHOD
    public void GainHealth()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth = currentHealth + 1;
        }
    }

    // TAKING DAMAGE METHOD
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }


    // RESPAWNING METHOD
    public void Respawn()
    {
        transform.position = respawnPoint;
        currentHealth = maxHealth;
        lives -= 1;
    }

    // SETTING THE SPAWN POINT METHOD
    public void SetSpawnPoint(Vector3 newPosition)
    {
        respawnPoint = newPosition;
    }

    // COLLECTING BONES METHOD
    public void Collectedbone()
    {
        boneCount = boneCount + 1;
    }

    // POWER UPS
    // Method that controls the triple jump power up
    public void TripleJump()
    {
        //Set timer to 10 seconds and max jumps to 3
        tripleJumpTimer = 10f;
        maxJumps = 3;
    }

    // Method that controls the speed power up
    public void SpeedPowerUp()
    {
        speedTimer = 10f;
        moveSpeed = 8;
    }

     void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "BreakingPlatform")
        {
            // Player moves down with the platform
            transform.Translate(new Vector3(0, -1 * breakingPlatform.fallSpeed * Time.deltaTime, 0));
        }
    }
}
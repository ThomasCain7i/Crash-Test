using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Health / lives / bones
    public int lives = 3;
    public int maxHealth = 3;
    public int currentHealth = 3;
    public int boneCount = 0;
    public float hitTimer;
    
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
    public GameObject speedGlow;

    // Respawn point
    public Vector3 respawnPoint;

    // Platforms
    private MovingPlatform movingPlatform;
    private BreakingPlatform breakingPlatform;

    // Direction
    public bool isFacingLeft;
    public bool isFacingRight;
    public bool isFacingForwards;
    public bool isFacingBackwards;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jumpsRemaining = maxJumps;
        currentHealth = maxHealth;

        movingPlatform = FindObjectOfType<MovingPlatform>();
        breakingPlatform = FindObjectOfType<BreakingPlatform>();
       
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
            isFacingLeft = true;
            isFacingRight = false;
            isFacingBackwards = false;
            isFacingForwards = false;
        }
        else if (moveHorizontal > 0)
        {
            transform.rotation = Quaternion.Euler(0f, 90f, 0f);
            isFacingRight = true;
            isFacingLeft = false;
            isFacingBackwards = false;
            isFacingForwards = false;
        }
        else if (moveVertical < 0)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            isFacingBackwards = true;
            isFacingRight = false;
            isFacingLeft = false;
            isFacingForwards = false;
        }
        else if (moveVertical > 0)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            isFacingForwards = true;
            isFacingRight = false;
            isFacingBackwards = false;
            isFacingLeft = false;
        }
        
        // Each direction is only active in one instance
       

        // If health = 0 - Thomas
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
 
        // Allows powerup to activate
        SuperSpeed();

        if (speedPowerup == true)
        {
           speedGlow.SetActive(true);
        }
        else if (speedPowerup == false)
        {
           speedGlow.SetActive(false);
        }
        
        // Time period for player's invincibility after hit
        hitTimer += Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            //Reset jumps and set grounded true - Thomas
            isGrounded = true;
            jumpsRemaining = maxJumps;
        }

        if (collision.gameObject.CompareTag("MovingPlatformLR"))
        {
            //Reset jumps and set grounded true - Thomas
            isGrounded = true;
            jumpsRemaining = maxJumps;
        }

        if (collision.gameObject.CompareTag("MovingPlatformFB"))
        {
            //Reset jumps and set grounded true - Thomas
            isGrounded = true;
            jumpsRemaining = maxJumps;
        }

        if (collision.gameObject.CompareTag("BreakingPlatform"))
        {
            //Reset jumps and set grounded true - Thomas
            isGrounded = true;
            jumpsRemaining = maxJumps;
        }
        
        if (collision.gameObject.CompareTag("Enemy"))
        {
            
           
            // Player takes damage
            if (hitTimer >= 3)
            {
               TakeDamage(1);
               hitTimer = 0;
            }

            // Player is invincible after hit
            if (hitTimer <= 3)
            {
                TakeDamage(0);
            }
           
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

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
       
    }

    public void Respawn()
    {
        transform.position = respawnPoint;
        currentHealth = maxHealth;
        lives -= 1;
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


            // Player becomes faster
            moveSpeed = 7.5f;
        }
        else if (speedPowerup == false)
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

     void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "MovingPlatformLR")
        {
            //Player moves along the platform
          
                if (movingPlatform.moveTimer <= 5.0f)
                {
                    // Player moves right changes from direction they are facing
                    
                    if (isFacingLeft == true)
                    {
                        transform.Translate(new Vector3(0, 0, -1 * Time.deltaTime));
                    }
                    else if (isFacingRight == true)
                    {
                       transform.Translate(new Vector3(0, 0, 1 * Time.deltaTime));
                    }
                    else if (isFacingBackwards == true)
                    {
                       transform.Translate(new Vector3(-1 * Time.deltaTime, 0, 0));
                    }
                    else if (isFacingForwards == true)
                    {
                      transform.Translate(new Vector3(1 * Time.deltaTime, 0, 0));       
                    }
                    
                }
                else if (movingPlatform.moveTimer <= 10.0f)
                {
                    // Player moves left changes from direction they are facing
                    
                    if (isFacingLeft == true)
                    {
                        transform.Translate(new Vector3(0, 0, 1 * Time.deltaTime));
                    }
                    else if (isFacingRight == true)
                    {
                       transform.Translate(new Vector3(0, 0, -1 * Time.deltaTime));
                    }
                    else if (isFacingBackwards == true)
                    {
                       transform.Translate(new Vector3(1 * Time.deltaTime, 0, 0));
                    }
                    else if (isFacingForwards == true)
                    {
                      transform.Translate(new Vector3(-1 * Time.deltaTime, 0, 0));       
                    }
                    
                }
            
           
        }
        else if (collision.gameObject.tag == "MovingPlatformFB")
        {
           
                if (movingPlatform.moveTimer <= 5.0f)
                {
                    // Player moves forward changes from direction they are facing
                    
                    if (isFacingLeft == true)
                    {
                       transform.Translate(new Vector3(1 * Time.deltaTime, 0, 0));
                    }
                    else if (isFacingRight == true)
                    {
                      transform.Translate(new Vector3(-1 * Time.deltaTime, 0, 0));
                    }
                    else if (isFacingBackwards == true)
                    {
                       transform.Translate(new Vector3(0, 0, -1 * Time.deltaTime));
                    }
                    else if (isFacingForwards == true)
                    {
                       transform.Translate(new Vector3(0, 0, 1 * Time.deltaTime));       
                    }
                    

                }
                else if (movingPlatform.moveTimer <= 10.0f)
                {
                    // Player moves backwards changes from direction they are facing
                    
                    if (isFacingLeft == true)
                    {
                       transform.Translate(new Vector3(-1 * Time.deltaTime, 0, 0));
                    }
                    else if (isFacingRight == true)
                    {
                      transform.Translate(new Vector3(1 * Time.deltaTime, 0, 0));
                    }
                    else if (isFacingBackwards == true)
                    {
                       transform.Translate(new Vector3(0, 0, 1 * Time.deltaTime));
                    }
                    else if (isFacingForwards == true)
                    {
                       transform.Translate(new Vector3(0, 0, -1 * Time.deltaTime));       
                    }
                    
                }
               
        }

        if (collision.gameObject.tag == "BreakingPlatform")
        {
            // Player moves down with the platform
            transform.Translate(new Vector3(0, -1 * breakingPlatform.fallSpeed * Time.deltaTime, 0));
        }
    }
}
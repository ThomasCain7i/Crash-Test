using UnityEngine;

public class PlayerControllerRIO : MonoBehaviour
{
    // Health / lives / bones
    public int lives = 3;
    public int maxHealth = 3;
    public int currentHealth = 3;
    public int boneCount = 0;
    public float hitTimer;
    public GameObject damagedBarrier; // Shows temporary invincibility from getting hit

    // Movement
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public int maxJumps = 2;
    private int jumpsRemaining;
    public float rotationSpeed;

    // Rigidbody
    private Rigidbody rb;
    private bool isGrounded;

    // Powerups
    public bool speedPowerup;
    public float speedTimer;
    public float maxSpeedTimer;
    public GameObject speedGlow;

    // Debuffs
    [Header("Debuffs")]
    public bool isSlowed = false;  // Indicates if the player is currently slowed down
    public bool isFrozen = false;  // Indicates if the player is currently frozen
    public float frozenTimer; // How long the player is frozen

    public bool starPowerup;
    public float starTimer;
    public GameObject starGlow;

    public bool megaPowerup;
    public float megaTimer;

    // Respawn point
    public Vector3 respawnPoint;

    // Platforms
    private MovingPlatform movingPlatform;
    private BreakingPlatform breakingPlatform;

    // Power Ups
    public float tripleJumpTimer;


    // Direction
    public bool isFacingLeft;
    public bool isFacingRight;
    public bool isFacingForwards;
    public bool isFacingBackwards;

    // Animation
    [Header("Animator")]
    public Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        jumpsRemaining = maxJumps;
        currentHealth = maxHealth;

        movingPlatform = FindObjectOfType<MovingPlatform>();
        breakingPlatform = FindObjectOfType<BreakingPlatform>();

        damagedBarrier.SetActive(false);
    }

    void Update()
    {
        // Input system movement - Thomas
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical) * moveSpeed;
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);

        // If the player isn't frozen, allow them to use movement
        if (!isFrozen)
        {
            // Jump with ground checker
            if (Input.GetButtonDown("Jump") && (isGrounded || jumpsRemaining > 0))
            {
                animator.SetBool("IsJumping", true);

                rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
                isGrounded = false;
                jumpsRemaining--;

            }
            else

            {
                if (isGrounded == true)
                {
                    animator.SetBool("IsJumping", false);
                    animator.SetBool("IsFalling", true);
                    animator.SetBool("IsDoubleJumping", false);
                }
                if (jumpsRemaining == 0)
                {
                    animator.SetBool("IsDoubleJumping", true);
                }
            }

            if (movement != Vector3.zero) //CHARACTER ROTATION //Setting up the rotation for the character
            {
                Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime); //Specifying how I want the character to rotate
                animator.SetBool("IsMoving", true);
            }
            else
            {
                animator.SetBool("IsMoving", false);

            }

            //PUNCH ATTACK - JUAN
            if (Input.GetMouseButton(0))
            {
                Debug.Log("PUNCH ATTACK");
                animator.SetBool("IsAttacking", true);

            }
            else
            {
                animator.SetBool("IsAttacking", false);
            }
        }

        tripleJumpTimer -= Time.deltaTime;

        if (tripleJumpTimer < 0)
        {
            maxJumps = 2;
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

        // Allows powerup to activate
        StarPower();

        if (starPowerup == true)
        {
            starGlow.SetActive(true);
        }
        else if (starPowerup == false)
        {
            starGlow.SetActive(false);
        }

        // Allows powerup to activate
        MegaPower();
        
        // Default speed with no power
        if (megaPowerup == false && speedPowerup == false)
        {
            moveSpeed = 5.0f;
        }
        
        // Neutralises speed with both powers active
        if (megaPowerup == true && speedPowerup == true)
        {
            moveSpeed = 5.0f;
        }

        // Time period for player's invincibility after hit
        hitTimer += Time.deltaTime;

        if (hitTimer <= 3)
        {
            damagedBarrier.SetActive(true);
        }
        else if (hitTimer >= 3)
        {
            damagedBarrier.SetActive(false);
        }
       


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
            if (hitTimer >= 3 && starPowerup == false)
            {
                TakeDamage(1);
                hitTimer = 0;
                damagedBarrier.SetActive(false);
                starGlow.SetActive(false);
            }
            // Player is invincible after hit
           else if (hitTimer <= 3)
            {
                TakeDamage(0);
                damagedBarrier.SetActive(true);
            }

            if (starPowerup == true)
            {
                TakeDamage(0);
                starGlow.SetActive(true);
            }

        }

        if (collision.gameObject.CompareTag("BouncePad"))
        {
            // Boost the player when they touch the bounce pad
            rb.AddForce(Vector3.up * jumpForce * 1.25f, ForceMode.VelocityChange);
            

            //Reset jumps and set grounded true - Thomas
            isGrounded = true;
            jumpsRemaining = maxJumps - 1;
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

    // Method that controls the triple jump power up
    public void TripleJump()
    {
        //Set timer to 10 seconds and max jumps to 3
        tripleJumpTimer = 10f;
        maxJumps = 3;
    }

    public void StarPowerUpFunction()
    {
        // Activates star power
        starPowerup = true;

        // resets timer
        starTimer = 0;
    }

    public void MegaPowerUpFunction()
    {
        // Activates super size power
        megaPowerup = true;

        // resets timer
        megaTimer = 0;
    }


    public void SuperSpeed()
    {
        if (speedPowerup == true)
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
        if (speedTimer >= 15.0f)
        {
            // Power works for a limited time
            speedPowerup = false;

        }

    }

    public void StarPower()
    {
        if (starPowerup == true)
        {
            // Becomes invincible
            TakeDamage(0);
            starGlow.SetActive(true);
            
        }
        else if (starPowerup == false)
        {
            //Player takes damage as normal
            starGlow.SetActive(false);
        }
          
        starTimer += Time.deltaTime;
        if (starTimer >= 10.0f)
        {
            // Power works for a limited time
            starPowerup = false;
        }

    }

    public void MegaPower()
    {
        if (megaPowerup == true)
        {
            // Doubles size and becomes slower
            transform.localScale = new Vector3(2f, 2f, 2f);
            moveSpeed = 3.5f;
        }
        else if (megaPowerup == false)
        {
            // Player has standard size
            transform.localScale = new Vector3(1f, 1f, 1f);
            
        }

        megaTimer += Time.deltaTime;
        if (megaTimer >= 12.0f)
        {
            // Power works for a limited time
            megaPowerup = false;
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

        if (collision.gameObject.tag == "RotatingPlatformX1")
        {
            // Player moves with the platform
            // Rotates in a forwards and backwards circle motion
        }

        if (collision.gameObject.tag == "RotatingPlatformX2")
        {
            // Player moves with the platform
            // Rotates in a forwards and backwards circle motion
        }

        if (collision.gameObject.tag == "RotatingPlatformY1")
        {
            // Player moves with the platform
            // Rotates in a flat circle motion
        }

        if (collision.gameObject.tag == "RotatingPlatformY2")
        {
            // Player moves with the platform
            // Rotates in a flat circle motion
        }

        if (collision.gameObject.tag == "RotationPlatformZ1")
        {
            // Player moves with the platform
            // Rotates in a left and right circle motion
        }

        if (collision.gameObject.tag == "RotationPlatformZ2")
        {
            // Player moves with the platform
            // Rotates in a left and right circle motion
        }
    }
}
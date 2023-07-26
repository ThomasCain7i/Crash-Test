// Rio

using UnityEngine;

public class PlayerControllerRIO : MonoBehaviour
{
    // Health / lives / bones
    [Header("Health")]
    public int lives = 3;
    public int maxHealth = 3;
    public int currentHealth = 3;
    public int boneCount = 0;
    public float hitTimer;
    public GameObject damagedBarrier; // Shows temporary invincibility from getting hit
    

    // Movement
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float runTimer;
    public bool isRunning;
    public float jumpForce = 5f;
    public int maxJumps = 2;
    private int jumpsRemaining;
    public float rotationSpeed;
    public bool isBouncing;
    public float bounceTimer;

    // Rigidbody
    [Header("Rigidbody")]
    private Rigidbody rb;
    private bool isGrounded;

    // Powerups
    [Header("Powerups")]
    public bool speedPowerup;
    public float speedTimer;
    public float maxSpeedTimer;
    public GameObject speedGlow;

    public bool starPowerup;
    public float starTimer;
    public GameObject starGlow;

    public bool megaPowerup;
    public float megaTimer;

    // Debuffs
    [Header("Debuffs")]
    public bool isSlowed = false;  // Indicates if the player is currently slowed down
    public bool isFrozen = false;  // Indicates if the player is currently frozen
    public float frozenTimer; // How long the player is frozen

    // Respawn point
    [Header("RespwanPoint")]
    public Vector3 respawnPoint;

    // Platforms
    [Header("Platforms")]
    private MovingPlatform movingPlatform;
    private BreakingPlatform breakingPlatform;

    public RotatingObject rotatingObject;

    // Power Ups
    public float tripleJumpTimer;


    // Direction
    [Header("Direction")]
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

        rotatingObject = FindObjectOfType<RotatingObject>();

       
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
                    isBouncing = false;
                    isRunning = true;

                    
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

                if(isRunning == true)
                {
                    runTimer += Time.deltaTime;
                }
                
            }
            else
            {
                animator.SetBool("IsMoving", false);
                runTimer = 0;
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
            
            isFacingLeft = true;
            isFacingRight = false;
            isFacingBackwards = false;
            isFacingForwards = false;
        }
        else if (moveHorizontal > 0)
        {
            
            isFacingRight = true;
            isFacingLeft = false;
            isFacingBackwards = false;
            isFacingForwards = false;
        }
        else if (moveVertical < 0)
        {
           
            isFacingBackwards = true;
            isFacingRight = false;
            isFacingLeft = false;
            isFacingForwards = false;
        }
        else if (moveVertical > 0)
        {
            
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
            damagedBarrier.SetActive(false);
        }
        else if (speedPowerup == false)
        {
            speedGlow.SetActive(false);
        }
        
        if (hitTimer <= 3 && starPowerup == true)
        {
            damagedBarrier.SetActive(false);
        }

        // Allows powerup to activate - Rio
        StarPower();

        if (starPowerup == true)
        {
            starGlow.SetActive(true);
            damagedBarrier.SetActive(false);
        }
        else if (starPowerup == false)
        {
            starGlow.SetActive(false);
        }

        // Allows powerup to activate - Rio
        MegaPower();
        
        // Default speed with no power - Rio
        if (megaPowerup == false && speedPowerup == false)
        {
            moveSpeed = 5.0f;
        }
        
        // Neutralises speed with both powers active - Rio
        if (megaPowerup == true && speedPowerup == true)
        {
            moveSpeed = 5.0f;
        }

        // Time period for player's invincibility after hit - Rio
        hitTimer += Time.deltaTime;

        if (hitTimer <= 3)
        {
            damagedBarrier.SetActive(true);
        }
        else if (hitTimer >= 3)
        {
            damagedBarrier.SetActive(false);
        }

        // Player jumps everytime they bounce;
        if (isBouncing == true)
        {
            animator.SetBool("IsJumping", true);
            
        }

       if (isBouncing == false && isGrounded == true)
        {
           animator.SetBool("IsFalling", true);
           
        }

        // Player runs for a few seconds and starts dashing - Rio
        if (runTimer >= 2f)
        {
            SpeedBoost(2);
        }
        else if (runTimer <= 2f)
        {
            SpeedBoost(0);
        }

        // stops run timer until the player is on ground - Rio
        if (isGrounded == false)
        {
            isRunning = false;
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

        if (collision.gameObject.CompareTag("RotateLeftSideSpin"))
        {
            //Reset jumps and set grounded true - Thomas
            isGrounded = true;
            jumpsRemaining = maxJumps;
        }

        if (collision.gameObject.CompareTag("RotateRightSideSpin"))
        {
            //Reset jumps and set grounded true - Thomas
            isGrounded = true;
            jumpsRemaining = maxJumps;
        }

        if (collision.gameObject.CompareTag("RotateFrontSpin"))
        {
            //Reset jumps and set grounded true - Thomas
            isGrounded = true;
            jumpsRemaining = maxJumps;
        }

        if (collision.gameObject.CompareTag("RotateBackSpin"))
        {
            //Reset jumps and set grounded true - Thomas
            isGrounded = true;
            jumpsRemaining = maxJumps;
        }

        if (collision.gameObject.CompareTag("RotateClockwiseFlatSpin"))
        {
            //Reset jumps and set grounded true - Thomas
            isGrounded = true;
            jumpsRemaining = maxJumps;
        }

        if (collision.gameObject.CompareTag("RotateAnti-ClockwiseFlatSpin"))
        {
            //Reset jumps and set grounded true - Thomas
            isGrounded = true;
            jumpsRemaining = maxJumps;
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Player takes damage - Rio
            if (hitTimer >= 3 && starPowerup == false)
            {
                TakeDamage(1);
                hitTimer = 0;
                damagedBarrier.SetActive(false);
                starGlow.SetActive(false);
            }
            // Player is invincible after hit - Rio
            if (hitTimer <= 3)
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

    public void SpeedBoost(int speedBoost)
    {
        moveSpeed += speedBoost;
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
        // Activates super speed - Rio
        speedPowerup = true;

        // resets timer - Rio
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
        // Activates star power - Rio
        starPowerup = true;

        // resets timer - Rio
        starTimer = 0;
    }

    public void MegaPowerUpFunction()
    {
        // Activates super size power - Rio
        megaPowerup = true;

        // resets timer - Rio
        megaTimer = 0;
    }


    public void SuperSpeed()
    {
        if (speedPowerup == true)
        {
            // When power activates the timer starts - Rio
            speedTimer += Time.deltaTime;

            // Player becomes faster - Rio
            moveSpeed = 7.5f;
        }
        else if (speedPowerup == false)
        {
            // returns to default speed - Rio
            moveSpeed = 5f;
            speedTimer = 0;
        }

        
        if (speedTimer >= 15.0f)
        {
            // Power works for a limited time - Rio
            speedPowerup = false;

        }

    }

    public void StarPower()
    {
        if (starPowerup == true)
        {
            // When power activates the timer starts - Rio
            starTimer += Time.deltaTime;

            // Becomes invincible - Rio
            TakeDamage(0);
            starGlow.SetActive(true);
            damagedBarrier.SetActive(false);

            
        }
        else if (starPowerup == false)
        {
            //Player takes damage as normal - Rio
            starGlow.SetActive(false);
            starTimer = 0;
        }
          
        
        if (starTimer >= 15.0f)
        {
            // Power works for a limited time - Rio
            starPowerup = false;
        }

    }

    public void MegaPower()
    {
        if (megaPowerup == true)
        {
            // When power activates the timer starts - Rio
            megaTimer += Time.deltaTime;
            
            // Shows the player growing slightly - Rio
            if (megaTimer >= 0.1f)
            {
                transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);   
            }

            // Doubles size and becomes slower - Rio
            if (megaTimer >= 0.35f)
            {
                transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            }
            moveSpeed = 3.5f;
        }
        else if (megaPowerup == false)
        {
            // Player has standard size - Rio
            transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            megaTimer = 0;
            
        }

        if (megaTimer >= 14.85f)
        {
            transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
        }

        if (megaTimer >= 15.0f)
        {
            // Power works for a limited time - Rio
            megaPowerup = false; 
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("BouncePad"))
        {
           
            bounceTimer += Time.deltaTime;

            if(bounceTimer >= 0.1)
            {
                // Boost the player when they touch the bounce pad for a brief moment - Rio
                rb.AddForce(Vector3.up * jumpForce * 0.9f, ForceMode.VelocityChange);
                jumpsRemaining = maxJumps - 1;
            }

            if (bounceTimer >= 0.05)
            {
                isGrounded = false;
                isBouncing = true;
            }

            //Reset jumps and set grounded true - Thomas

            isGrounded = true;
            isBouncing = false;
            
        }

        if (collision.gameObject.tag == "MovingPlatformLR")
        {
            //Player moves along the platform - Rio

            if (movingPlatform.moveTimer <= 8.0f)
            {
                // Player moves right changes from direction they are facing - Rio

                if (isFacingLeft == true)
                {
                    transform.Translate(new Vector3(0, 0, -1.5f * Time.deltaTime));
                }
                else if (isFacingRight == true)
                {
                    transform.Translate(new Vector3(0, 0, 1.5f * Time.deltaTime));
                }
                else if (isFacingBackwards == true)
                {
                    transform.Translate(new Vector3(-1.5f * Time.deltaTime, 0, 0));
                }
                else if (isFacingForwards == true)
                {
                    transform.Translate(new Vector3(1.5f * Time.deltaTime, 0, 0));
                }

            }
            else if (movingPlatform.moveTimer <= 16.0f)
            {
                // Player moves left changes from direction they are facing - Rio

                if (isFacingLeft == true)
                {
                    transform.Translate(new Vector3(0, 0, 1.5f * Time.deltaTime));
                }
                else if (isFacingRight == true)
                {
                    transform.Translate(new Vector3(0, 0, -1.5f * Time.deltaTime));
                }
                else if (isFacingBackwards == true)
                {
                    transform.Translate(new Vector3(1.5f * Time.deltaTime, 0, 0));
                }
                else if (isFacingForwards == true)
                {
                    transform.Translate(new Vector3(-1.5f * Time.deltaTime, 0, 0));
                }

            }


        }
        else if (collision.gameObject.tag == "MovingPlatformFB")
        {

            if (movingPlatform.moveTimer <= 8.0f)
            {
                // Player moves forward changes from direction they are facing - Rio

                if (isFacingLeft == true)
                {
                    transform.Translate(new Vector3(1.5f * Time.deltaTime, 0, 0));
                }
                else if (isFacingRight == true)
                {
                    transform.Translate(new Vector3(-1.5f * Time.deltaTime, 0, 0));
                }
                else if (isFacingBackwards == true)
                {
                    transform.Translate(new Vector3(0, 0, -1.5f * Time.deltaTime));
                }
                else if (isFacingForwards == true)
                {
                    transform.Translate(new Vector3(0, 0, 1.5f * Time.deltaTime));
                }


            }
            else if (movingPlatform.moveTimer <= 16.0f)
            {
                // Player moves backwards changes from direction they are facing - Rio

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
            // Player moves down with the platform - Rio
            transform.Translate(new Vector3(0, -1 * breakingPlatform.fallSpeed * Time.deltaTime, 0));
        }

        if (collision.gameObject.tag == "RotateLeftSideSpin")
        {
            // Player moves with the platform - Rio
            // Rotates in a left to right circle motion - Rio

           if (isFacingLeft == true)
            {
                transform.Translate(new Vector3(0, 0, 1 * rotatingObject.rotateSpeed * 0.1f * Time.deltaTime));
            }
            else if (isFacingRight == true)
            {
               transform.Translate(new Vector3(0, 0, -1 * rotatingObject.rotateSpeed * 0.1f * Time.deltaTime));
            }
             else if (isFacingBackwards == true)
            {
                transform.Translate(new Vector3(1 * rotatingObject.rotateSpeed * 0.1f * Time.deltaTime, 0, 0));
           }
            else if (isFacingForwards == true)
            {
                transform.Translate(new Vector3(-1 * rotatingObject.rotateSpeed * 0.1f * Time.deltaTime, 0, 0));
            }
        }

        if (collision.gameObject.tag == "RotateRightSideSpin")
        {
            // Player moves with the platform - Rio
            // Rotates in a right to left circle motion - Rio

           if (isFacingLeft == true)
            {
                transform.Translate(new Vector3(0, 0, -1 * rotatingObject.rotateSpeed * 0.1f * Time.deltaTime));
            }
            else if (isFacingRight == true)
            {
                transform.Translate(new Vector3(0, 0, 1 * rotatingObject.rotateSpeed * 0.1f * Time.deltaTime));
            }
            else if (isFacingBackwards == true)
            {
                transform.Translate(new Vector3(-1 * rotatingObject.rotateSpeed * 0.1f * Time.deltaTime, 0, 0));
            }
            else if (isFacingForwards == true)
            {
                transform.Translate(new Vector3(1 * rotatingObject.rotateSpeed * 0.1f * Time.deltaTime, 0, 0));
            }
        }


        if (collision.gameObject.tag == "RotateFrontSpin")
        {
            // Player moves with the platform - Rio
            // Rotates in a forwards to backwards circle motion - Rio

            if (isFacingLeft == true)
            {
                transform.Translate(new Vector3(1 * rotatingObject.rotateSpeed * 0.1f * Time.deltaTime, 0, 0));
            }
            else if (isFacingRight == true)
            {
                transform.Translate(new Vector3(-1 * rotatingObject.rotateSpeed * 0.1f * Time.deltaTime, 0, 0));
            }
            else if (isFacingBackwards == true)
            {
                transform.Translate(new Vector3(0, 0, -1 * rotatingObject.rotateSpeed * 0.1f * Time.deltaTime));
            }
            else if (isFacingForwards == true)
            {
                transform.Translate(new Vector3(0, 0, 1 * rotatingObject.rotateSpeed * 0.1f * Time.deltaTime));
            }
        }

        if (collision.gameObject.tag == "RotateBackSpin")
        {
            // Player moves with the platform - Rio
            // Rotates in a backwards to forwards circle motion - Rio

            if (isFacingLeft == true)
            {
                transform.Translate(new Vector3(-1 * rotatingObject.rotateSpeed * 0.1f * Time.deltaTime, 0, 0));
            }
            else if (isFacingRight == true)
            {
                transform.Translate(new Vector3(1 * rotatingObject.rotateSpeed * 0.1f * Time.deltaTime, 0, 0));
            }
            else if (isFacingBackwards == true)
            {
                transform.Translate(new Vector3(0, 0, 1 * rotatingObject.rotateSpeed * 0.1f * Time.deltaTime));
            }
            else if (isFacingForwards == true)
            {
                transform.Translate(new Vector3(0, 0, -1 * rotatingObject.rotateSpeed * 0.1f * Time.deltaTime));
            }
        }

        if (collision.gameObject.tag == "RotateClockwiseFlatSpin")
        {
            // Player moves with the platform - Rio
            // Rotates in a flat circle motion - Rio

            Vector3 point = new Vector3(-12.92f,8.61f,100.08f); // Based on position of rotating platform - Rio
            Vector3 axis =  new Vector3(0,1f,0);
            transform.RotateAround(point, axis, Time.deltaTime * rotatingObject.rotateSpeed);
           
            
        }

        if (collision.gameObject.tag == "RotateAnti-ClockwiseFlatSpin")
        {
            // Player moves with the platform - Rio
            // Rotates in a flat circle motion - Rio

            Vector3 point = new Vector3(6.5f,0f,18.6f); // Based on position of rotating platform - Rio
            Vector3 axis =  new Vector3(0,-1f,0);
            transform.RotateAround(point, axis, Time.deltaTime * rotatingObject.rotateSpeed);
        }

       
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "BouncePad")
        {
            // Makes the player jump after being on the platform for a brief moment - Rio
            isBouncing = true;
            isGrounded = false;

            // Resets timer so that the player doesn't intantly bounce on the bounce pad - Rio
            bounceTimer = 0;
        }
            
        
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "EnergyBeam")
        {
             // Player takes damage - Rio
            if (hitTimer >= 3 && starPowerup == false)
            {
                TakeDamage(1);
                hitTimer = 0;
                damagedBarrier.SetActive(false);
                starGlow.SetActive(false);
            }
            // Player is invincible after hit - Rio
            if (hitTimer <= 3)
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
    }
}
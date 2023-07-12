using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Health / Lives / Armor
    [Header("Health / Lives")]
    public int lives = 3;  // Number of lives the player has
    public int maxHealth = 3;  // Maximum health the player can have
    public float currentHealth = 3;  // Current health of the player
    public int Armour = 0;

    // Attacks
    [Header("Attacks")]
    public float barkDamage = 3;  // Damage caused by bark attack
    public float smashDamage = 3;  // Damage caused by smash attack
    public float punchDamage = 3;  // Damage caused by smash attack
    [SerializeField]
    private GameObject punchPrefab;  // Speed for punch
    [SerializeField]
    private Transform punchPos; // Location of Punch

    // Bones
    [Header("Bones")]
    public int bonusCount = 0;  // Number of collected bones

    // Movement
    [Header("Movement")]
    public float moveSpeed = 5f;  // Movement speed of the player
    public float normalMoveSpeed = 5f;  // Normal movement speed of the player
    public float jumpForce = 5f;  // Force applied when the player jumps
    public int maxJumps = 2;  // Maximum number of jumps the player can perform
    [SerializeField]
    private int jumpsRemaining = 2;  // Number of jumps remaining for the player
    public float rotationSpeed;
    private bool thirdJump = false;

    // Rigidbody / Ground test
    [Header("Rigidbody / Ground Test")]
    private Rigidbody rb;  // Reference to the Rigidbody component of the player
    private bool isGrounded;  // Indicates if the player is currently grounded

    // Powerups
    [Header("Power Ups")]
    public float normalSpeedTimer = 10f;  // The timer will be set to this after being picked up
    public float normalTripleJumpTimer = 10f;  // The timer will be set to this after being picked up
    public float normalTimeSlowTimer = 2.5f;  // The timer will be set to this after being picked up
    public float timeSlow = 0.75f;
    private float speedTimer;  // Timer for the speed power-up
    private float tripleJumpTimer;  // Timer for the triple jump power-up
    private float timeSlowTimer;  // Timer for the Time Slow power-up

    // Debuffs
    [Header("Debuffs")]
    public bool isSlowed = false;  // Indicates if the player is currently slowed down
    public bool isFrozen = false;  // Indicates if the player is currently frozen
    public float frozenTimer; // How long the player is frozen

    // Respawn point
    [Header("Respawn Point")]
    public Vector3 respawnPoint;  // Position where the player respawns after dying

    // Animation
    [Header("Animator")]
    public Animator animator;

    // References
    [Header("References")]
    private AttackScript attackScript; // Reference to the Attack script 
    public UIManager uiManager;  // Reference to the UIManager script
    private BreakingPlatform breakingPlatform;  // Reference to the BreakingPlatform script

    void Start()
    {
        // Get the Rigidbody and animator components of the player
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        uiManager = FindObjectOfType<UIManager>(); // Find and assign the UI Manager script in the scene
        attackScript = GetComponent<AttackScript>();

        //Health UI
        uiManager.healthText.text = "Health: " + currentHealth.ToString();

        // Set jumps and health to max
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

        // If the player isn't frozen, allow them to use movement
        if (!isFrozen)
        {
            // Jump with ground checker
            if (Input.GetButtonDown("Jump") && (isGrounded || jumpsRemaining > 0))
            {
                animator.SetBool("IsJumping", true);

                rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
                isGrounded = false;
                jumpsRemaining --;
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

            if (attackScript.snow == 1 && Input.GetButtonDown("Jump") && attackScript.platform == false && thirdJump == true)
            {
                attackScript.platform = true;
                Instantiate(attackScript.snowPrefab, attackScript.smashPoint.position, attackScript.smashPoint.rotation);
            }

            if (jumpsRemaining == 0)
            {
                thirdJump = true;
            }
            else
            {
                thirdJump = false;
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
                animator.SetBool("IsAttacking", false);
                animator.SetBool("IsBarking", false);

            }

            //PUNCH ATTACK - JUAN
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("PUNCH ATTACK");
                animator.SetBool("IsAttacking", true);
                animator.SetTrigger("Attack");
                Instantiate(punchPrefab, punchPos.position, punchPos.rotation);
            }
            else
            {
                animator.SetBool("IsAttacking", false);
            }
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }

        // POWER UPS
        // Timer Control
        tripleJumpTimer -= Time.deltaTime;
        speedTimer -= Time.deltaTime;
        timeSlowTimer -= Time.deltaTime;

        // Triple jump powerup
        if (tripleJumpTimer <= 0)
        {
            maxJumps = 2;
        }

        if (speedTimer < 0 && !isSlowed && !isFrozen)
        {
            moveSpeed = normalMoveSpeed;
            isFrozen = false;
        }

        if (timeSlowTimer < 0)
        {
            Time.timeScale = 1f;
        }

        // DEBUFFS
        frozenTimer -= Time.deltaTime;
        // Frozen
        if (frozenTimer > 0)
        {
            isFrozen = true;
            moveSpeed = 0f;
        }
        else
        {
            isFrozen = false;
        }
    }

    // HEALTH / LIVES
    // Decreases the player's health by the specified amount
    public void TakeDamage(float amount)
    {
        if (Armour < 1)
        {
            currentHealth -= amount;
            uiManager.ArmourUIoff();
            uiManager.HealthUI();
        }
        else
        {
            Armour = 0;
            uiManager.ArmourBrokenUI();
        }

        if (lives >= 1)
        {
            if (currentHealth <= 0)
            {
                // Respawn the player
                Respawn();
            }
        }
        else
        {
            // End game
            Debug.Log("Game over - Lives depleted");
        }
    }

    // Resetting jumps when touching the ground
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Reset jumps and set grounded to true
            isGrounded = true;
            jumpsRemaining = maxJumps;
            attackScript.platform = false;
        }

        if (collision.gameObject.CompareTag("SnowGround"))
        {
            isGrounded = true;
            jumpsRemaining += 1;
            attackScript.smashing = false;
        }
    }

    // GAINING HEALTH METHOD- Camron
    // Increases the player's health by 1, up to the maximum health
    public void GainHealth()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += 1;
        }
    }

    // TAKING DAMAGE METHOD
    // Decreases the player's health by the specified damage amount
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }

    // RESPAWNING METHOD
    // Respawns the player at the designated respawn point
    public void Respawn()
    {
        transform.position = respawnPoint;
        currentHealth = maxHealth;
        lives -= 1;
    }

    // SETTING THE SPAWN POINT METHOD
    // Sets the player's respawn point to the set position
    public void SetSpawnPoint(Vector3 newPosition)
    {
        respawnPoint = newPosition;
    }

    // COLLECTING BONES METHOD- Camron
    // Increases the player's bone count by 1
    public void CollectedBone()
    {
        bonusCount += 1;
    }

    // POWER UPS
    // Sets the timer and maximum jumps for the triple jump power-up
    public void TripleJumpPowerUp()
    {
        uiManager.TripleJumpUI();
        tripleJumpTimer = normalTripleJumpTimer;
        maxJumps = 3;
        jumpsRemaining = maxJumps;
    }

    // Activates the speed power-up for a certain duration
    public void SpeedPowerUp()
    {
        uiManager.SpeedUI();
        speedTimer = normalSpeedTimer;
        moveSpeed = 8f;
    }

    // Activates the time slow power-up for a certain duration
    public void TimeSlowPowerUp()
    {
        uiManager.SlowMoUI();
        timeSlowTimer = normalTimeSlowTimer;
        Time.timeScale = timeSlow;
    }

    // DEBUFFS
    // Freezes the player for a specified duration
    public void Frozen()
    {
        frozenTimer = 2f;
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
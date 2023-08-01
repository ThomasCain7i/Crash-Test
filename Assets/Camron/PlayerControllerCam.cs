using UnityEngine;

public class PlayerControllerCam : MonoBehaviour
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
    public int BonusCount = 0;  // Number of collected bones
    public int SandBonusCount = 0;  // Number of collected bones
    public int WaterBonusCount = 0;  // Number of collected bones
    public int SnowBonusCount = 0;  // Number of collected bones
    public int FireBonusCount = 0;  // Number of collected bones

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
    [SerializeField]
    private bool isMoving;
    [SerializeField]
    private Transform cameraTransform;

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
    [SerializeField]
    private float walkTimer;  // Timer for the Time Slow power-up

    // Debuffs
    [Header("Debuffs")]
    public bool isSlowed = false;  // Indicates if the player is currently slowed down
    public bool isFrozen = false;  // Indicates if the player is currently frozen
    public float frozenTimer; // How long the player is frozen

    // Respawn point
    [Header("Respawn Point")]
    public Vector3 respawnPoint;  // Position where the player respawns after dying
    [SerializeField]
    private Transform SecondCheckpoint;
    [SerializeField]
    private Transform startCheckpoint;

    // Animation
    [Header("Animator")]
    public Animator animator;

    // References
    [Header("References")]
    private AttackScript attackScript; // Reference to the Attack script 
    public UIManager uiManager;  // Reference to the UIManager script
    private BreakingPlatform breakingPlatform;  // Reference to the BreakingPlatform script
    private GameManager gameManager;
    private SoundPlayer soundPlayer;
    private SoundFootsteps soundFootsteps;
    private CameraFollow cameraFollow;

    void Start()
    {
        // Get the Rigidbody and animator components of the player
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        uiManager = FindObjectOfType<UIManager>(); // Find and assign the UI Manager script in the scene
        attackScript = GetComponent<AttackScript>();
        soundPlayer = FindObjectOfType<SoundPlayer>();
        soundFootsteps = FindObjectOfType<SoundFootsteps>();
        cameraFollow = FindObjectOfType<CameraFollow>();

        //Health UI
        uiManager.healthText.text = "Health: " + currentHealth.ToString();

        // Set jumps and health to max
        jumpsRemaining = maxJumps;
        currentHealth = maxHealth;

        // Get ref to gameManager and load settings
        gameManager = FindObjectOfType<GameManager>();
        gameManager.LoadSettings();
    }

    void Update()
    {
        BonusCount = FireBonusCount + SandBonusCount + WaterBonusCount + SnowBonusCount;

        // MOVEMENT
        // Input system movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        camForward.y = 0;
        camRight.y = 0;

        Vector3 forwardRelative = moveVertical * camForward;
        Vector3 rightRelative = moveHorizontal * camRight;
        Vector3 movementDirection = forwardRelative + rightRelative;
        Vector3 movement = movementDirection * moveSpeed;


        var targetAngle = Mathf.Atan2(movementDirection.x, movementDirection.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, targetAngle, 0.0f);

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

                Debug.Log("Sound");
                soundPlayer.PlayJump();
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

            if (movementDirection != Vector3.zero) //CHARACTER ROTATION //Setting up the rotation for the character
            {
                isMoving = true;
                Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime); //Specifying how I want the character to rotate
                animator.SetBool("IsMoving", true);
            }
            else
            {
                animator.SetBool("IsMoving", false);
                animator.SetBool("IsAttacking", false);
                animator.SetBool("IsBarking", false);
                isMoving = false;
            }

            //PUNCH ATTACK - JUAN
            if (Input.GetMouseButtonDown(0))
            {
                soundPlayer.PlayPunch();
                Debug.Log("PUNCH ATTACK");
                animator.SetBool("IsAttacking", true);
                animator.SetTrigger("Attack");
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

        if (isMoving && walkTimer <= 0 && isGrounded)
        {
            soundFootsteps.PlayWalk();
            walkTimer = .6f;
        }

        // POWER UPS
        // Timer Control
        tripleJumpTimer -= Time.deltaTime;
        speedTimer -= Time.deltaTime;
        timeSlowTimer -= Time.deltaTime;
        walkTimer -= Time.deltaTime;

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
            Debug.Log("0");
            currentHealth -= amount;
            Debug.Log("1");
            soundPlayer.PlayDamaged();
            Debug.Log("2");
            uiManager.ArmourUIoff();
            Debug.Log("3");
            uiManager.HealthUI();
            Debug.Log("4");
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
                cameraFollow.threeD = true;
                cameraFollow.twoD = false;
                soundPlayer.PlayDeath();
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
            soundPlayer.PlayHealing();
            currentHealth += 1;
        }
    }

    // TAKING DAMAGE METHOD
    // Decreases the player's health by the specified damage amount
    //public void TakeDamage(int damage)
    //{
    //    currentHealth -= damage;
    //}

    // RESPAWNING METHOD
    // Respawns the player at the designated respawn point
    public void Respawn()
    {
        soundPlayer.PlayRespawn();
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
    public void SandCollectedBonus()
    {
        SandBonusCount += 1;
        soundPlayer.PlayElemental();
    }
    public void WaterCollectedBonus()
    {
        WaterBonusCount += 1;
        soundPlayer.PlayElemental();
    }
    public void FireCollectedBonus()
    {
        FireBonusCount += 1;
        soundPlayer.PlayElemental();
    }
    public void SnowCollectedBonus()
    {
        SnowBonusCount += 1;
        soundPlayer.PlayElemental();
    }

    // POWER UPS
    // Sets the timer and maximum jumps for the triple jump power-up
    public void TripleJumpPowerUp()
    {
        uiManager.TripleJumpUI();
        soundPlayer.PlayTripleJump();
        tripleJumpTimer = normalTripleJumpTimer;
        maxJumps = 3;
        jumpsRemaining = maxJumps;
    }

    // Activates the speed power-up for a certain duration
    public void SpeedPowerUp()
    {
        soundPlayer.PlaySpeedBoost();
        uiManager.SpeedUI();
        speedTimer = normalSpeedTimer;
        moveSpeed = 8f;
    }

    // Activates the time slow power-up for a certain duration
    public void TimeSlowPowerUp()
    {
        soundPlayer.PlaySlowMo();
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
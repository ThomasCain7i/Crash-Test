using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    // Health / Lives / Armor
    [Header("Health / Lives")]
    public int lives = 3;  // Number of lives the player has
    public int maxHealth = 3;  // Maximum health the player can have
    public float currentHealth = 3;  // Current health of the player
    public int Armour = 0;
    [SerializeField]
    private bool isAlive = true;
    [SerializeField]
    public float hitTimer;

    // Attacks
    [Header("Attacks")]
    public float barkDamage = 3;  // Damage caused by bark attack
    public float fireBarkDamage = 3;  // Damage caused by bark attack
    public float smashDamage = 3;  // Damage caused by smash attack

    // Bones
    [Header("Bones")]
    public int BonusCount = 0;  // Number of collected bones
    public int SandBonusCount = 0;  // Number of collected bones
    public int WaterBonusCount = 0;  // Number of collected bones
    public int SnowBonusCount = 0;  // Number of collected bones
    public int FireBonusCount = 0;  // Number of collected bones

    // Movement
    [Header("Movement")]
    public Vector3 movement;
    public float moveSpeed = 5f;  // Movement speed of the player
    public float normalMoveSpeed = 5f;  // Normal movement speed of the player
    public float jumpForce = 5f;  // Force applied when the player jumps
    public int maxJumps = 2;  // Maximum number of jumps the player can perform
    [SerializeField]
    private int jumpsRemaining = 2;  // Number of jumps remaining for the player
    public float rotationSpeed;
    private bool thirdJump = false;
    [SerializeField]
    private bool isMoving, isBouncing;
    public float bounceTimer;
    [SerializeField]
    private Transform cameraTransform;
    public bool lava;

    // Rigidbody / Ground test
    [Header("Rigidbody / Ground Test")]
    private Rigidbody rb;  // Reference to the Rigidbody component of the player
    public bool isGrounded;  // Indicates if the player is currently grounded

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

    //UI
    [Header("UI")]
    [SerializeField]
    private GameObject deathScreen;

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
    private PauseMenu pauseMenu;


    //SWIMMING BOOLS - JUAN

    public bool isFloating;
    public bool isSwimming;
    public bool game2D; 
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Camron Scene")
        {
            lava = true;
        }
        // Get the Rigidbody and animator components of the player
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        uiManager = FindObjectOfType<UIManager>(); // Find and assign the UI Manager script in the scene
        attackScript = GetComponent<AttackScript>();
        soundPlayer = FindObjectOfType<SoundPlayer>();
        soundFootsteps = FindObjectOfType<SoundFootsteps>();
        cameraFollow = FindObjectOfType<CameraFollow>();
        pauseMenu = FindObjectOfType<PauseMenu>();

        //Health UI
        uiManager.healthText.text = "Health: " + currentHealth.ToString();

        // Set jumps and health to max
        jumpsRemaining = maxJumps;
        currentHealth = maxHealth;

        // Get ref to gameManager and load settings
        gameManager = FindObjectOfType<GameManager>();
        gameManager.LoadSettings();

        Debug.Log("Loaded Player Controller");

        //SWIMMING BOOLS - JUAN

        isFloating = false;
        isSwimming = false;

        
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


        var targetAngle = Mathf.Atan2(movementDirection.x, movementDirection.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, targetAngle, 0.0f);

        if (lava)
        {
            movement = movementDirection * moveSpeed;
        }
        else
        {
            movement = new Vector3(moveHorizontal, 0f, moveVertical) * moveSpeed;
        }
        
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


            

            //SWIMMING JUAN


            if (isFloating == true)
            {
                if (movement != Vector3.zero) //CHARACTER ROTATION //Setting up the rotation for the character
                {
                    Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime); //Specifying how I want the character to rotate
                    animator.SetBool("IsSwimming", true);
                    isSwimming = true;

                }
                else
                {
                    isSwimming = false;
                    animator.SetBool("IsSwimming", false);
                }
            }

            if ((isFloating == true) && lava)
            {
                if (movementDirection != Vector3.zero) //CHARACTER ROTATION //Setting up the rotation for the character
                {
                    Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime); //Specifying how I want the character to rotate
                    animator.SetBool("IsSwimming", true);
                    isSwimming = true;

                }
                else
                {
                    isSwimming = false;
                    animator.SetBool("IsSwimming", false);
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
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }

        if(isMoving && walkTimer <= 0 && isGrounded) 
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
        hitTimer -= Time.deltaTime;

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

        if (timeSlowTimer < 0 && isAlive && !pauseMenu.isPaused)
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
        if (Armour < 1 && hitTimer <= 0)
        {
            currentHealth -= amount;
            soundPlayer.PlayDamaged();
            uiManager.ArmourUIoff();
            uiManager.HealthUI();
            hitTimer = 2;
        }
        
        if (Armour == 1)
        {
            Armour = 0;
            uiManager.ArmourBrokenUI();
        }

        if (lives >= 1)
        {
            if (currentHealth <= 0)
            {
                cameraFollow.oneD = true;
                cameraFollow.twoD = false;
                cameraFollow.threeD = false;
                cameraFollow.fourD = false;
                cameraFollow.maze = false;
                soundPlayer.PlayDeath();
                // Respawn the player
                Respawn();
            }
        }

        if (lives == 0)
        {
            if (currentHealth <= 0)
            {
                isAlive = false;
                Time.timeScale = 0f;
                deathScreen.SetActive(true);
                // End game
                Debug.Log("Game over - Lives depleted");
            }
        }
    }

    // Resetting jumps when touching the ground
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            soundPlayer.PlayLand();
            Debug.Log("Play Land");

            isGrounded = true;
            jumpsRemaining = maxJumps;
            attackScript.platform = false;
        }

        if (collision.gameObject.CompareTag("SnowGround"))
        {
            if (!attackScript.smashing)
            {
                soundPlayer.PlayLand();
            }

            isGrounded = true;
            jumpsRemaining += 1;
            attackScript.smashing = false;
        }
        //SWIMMING - JUAN
        if (collision.gameObject.CompareTag("Water"))
        {
            GetComponent<CapsuleCollider>().direction = 2;
            isFloating = true;

            animator.SetBool("IsFloating", true);
        }
        else
        {
            GetComponent<CapsuleCollider>().direction = 1;
            isFloating = false;
            animator.SetBool("IsFloating", false);
            animator.SetBool("IsSwimming", false);
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
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }

    // RESPAWNING METHOD
    // Respawns the player at the designated respawn point
    public void Respawn()
    {
        
        transform.position = respawnPoint;
        soundPlayer.PlayRespawn();
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
        moveSpeed += 2;
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

        if (collision.gameObject.CompareTag("BouncePad"))
        {
            soundPlayer.PlayBouncePad();

            bounceTimer += Time.deltaTime;

            if (bounceTimer >= 0.1)
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
    }
}
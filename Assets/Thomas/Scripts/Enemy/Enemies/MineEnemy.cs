using UnityEngine;
using UnityEngine.AI;

public class MineEnemy : MonoBehaviour
{
    [Header("References")]
    public Transform player; // Reference to the player's transform

    public LayerMask whatIsGround, whatIsPlayer; // Layer masks for ground and player

    [Header("Stats")]
    public float damage; // Damage caused by the enemy

    // Patrolling
    [Header("Patrolling")]
    public Transform[] patrolPoints; // Array of patrol points
    private int currentPatrolIndex; // Index of the current patrol point

    // Attacking
    [Header("Attacking")]
    public GameObject explosionPrefab; // Prefab for the explosion effect
    public Transform targetTransform; // Reference to the target transform for the explosion
    public float distanceFromPlayer; // Distance between the enemy and the player
    [SerializeField]
    private float countdownTimer, countDownTimerNormal; // Timer for the countdown
    [SerializeField]
    private bool countdownBool = false; // Flag indicating if the countdown is active

    // States
    [Header("States")]
    public float sightRange; // Range for detecting the player
    public float attackRange; // Range for attacking the player
    public bool playerInSightRange, playerInAttackRange; // Flags indicating if the player is within sight range and attack range

    private void Awake()
    {
        player = GameObject.Find("Player").transform; // Find and assign the player's transform
    }

    private void Update()
    {
        // Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer); // Check if the player is within sight range
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer); // Check if the player is within attack range

        if (playerInAttackRange)
        {
            AttackPlayer(); // If the player is in attack range and sight range, attack the player
        }

        if (countdownTimer > 0)
        {
            countdownTimer -= Time.deltaTime; // Reduce the countdown timer

            if (countdownTimer < 0)
            {
                Explode(); // Call the Explode method
            }
        }
    }

    // Attacking the player
    private void AttackPlayer()
    {
        if (countdownBool == false)
        {
            Countdown(); // Start the countdown if it is not already active
        }
    }

    // Start the countdown timer
    void Countdown()
    {
        countdownBool = true; // Set the countdown flag to true
        countdownTimer = countDownTimerNormal; // Set the countdown timer to 3 seconds
    }

    // Explode and destroy the enemy
    void Explode()
    {
        Instantiate(explosionPrefab, targetTransform.position, Quaternion.identity); // Spawn the explosion prefab at the target transform's position
        Destroy(gameObject); // Destroy the enemy game object
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange); // Draw a wire sphere representing the attack range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange); // Draw a wire sphere representing the sight range
    }
}
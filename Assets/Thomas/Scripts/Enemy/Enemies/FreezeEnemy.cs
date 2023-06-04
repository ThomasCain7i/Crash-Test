using UnityEngine;
using UnityEngine.AI;

public class FreezeEnemy : MonoBehaviour
{
    [Header("References")]
    public NavMeshAgent agent; // Reference to the NavMeshAgent component
    public Transform player; // Reference to the player's transform

    public LayerMask whatIsGround, whatIsPlayer; // Layer masks for ground and player

    [Header("Stats")]
    public float speed; // Movement speed of the enemy
    public float damage; // Damage caused by the enemy

    //Patrolling
    [Header("Patrolling")]
    public Transform[] patrolPoints; // Array of patrol points
    private int currentPatrolIndex; // Index of the current patrol point

    //Attacking
    [Header("Attacking")]
    public float timeBetweenAttacks; // Time between attacks
    bool alreadyAttacked; // Flag indicating if the enemy has already attacked
    public GameObject projectile; // Projectile game object for attacking
    public float projectileSpeed; // Speed of the projectile
    public Transform rangedAttackPoint; // Point to spawn the projectile

    //States
    [Header("States")]
    public float sightRange; // Range for detecting the player
    public float attackRange; // Range for attacking the player
    public bool playerInSightRange, playerInAttackRange; // Flags indicating if the player is within sight range and attack range

    private void Awake()
    {
        player = GameObject.Find("Player").transform; // Find and assign the player's transform
        agent = GetComponent<NavMeshAgent>(); // Get the NavMeshAgent component of the enemy
        agent.speed = speed; // Set the movement speed of the enemy
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer); // Check if the player is within sight range
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer); // Check if the player is within attack range

        if (!playerInSightRange && !playerInAttackRange)
            Patroling(); // If the player is not in sight or attack range, patrol
        else if (playerInSightRange && !playerInAttackRange)
            ChasePlayer(); // If the player is in sight range but not attack range, chase the player
        else if (playerInAttackRange && playerInSightRange)
            AttackPlayer(); // If the player is in attack range and sight range, attack the player
    }

    private void Patroling()
    {
        if (patrolPoints.Length == 0)
        {
            Debug.LogWarning("No patrol points assigned!"); // Log a warning if no patrol points are assigned
            return;
        }

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            // Reached the current patrol point, move to the next one
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length; // Increment the patrol point index
            agent.SetDestination(patrolPoints[currentPatrolIndex].position); // Set the destination to the next patrol point
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position); // Set the destination to the player's position
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position); // Set the destination to the current position of the enemy

        transform.LookAt(player); // Make the enemy face the player

        if (!alreadyAttacked)
        {
            ///Attack code here
            // Instantiate the projectile and get its Rigidbody component
            Rigidbody rb = Instantiate(projectile, rangedAttackPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * projectileSpeed, ForceMode.Impulse); // Add force to the projectile in the forward direction
            rb.AddForce(transform.up * 4f, ForceMode.Impulse); // Add upward force to the projectile for trajectory
            ///End of attack code

            alreadyAttacked = true; // Set the alreadyAttacked flag to true
            Invoke(nameof(ResetAttack), timeBetweenAttacks); // Call ResetAttack method after the specified time
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false; // Reset the alreadyAttacked flag
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange); // Draw a wire sphere representing the attack range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange); // Draw a wire sphere representing the sight range
    }
}
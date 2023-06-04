using UnityEngine;
using UnityEngine.AI;

public class CloseEnemy : MonoBehaviour
{
    [Header("References")]
    public NavMeshAgent agent; // Reference to the NavMeshAgent component
    public Transform player; // Reference to the player's transform

    public LayerMask whatIsGround, whatIsPlayer; // Layer masks for ground and player

    [Header("Stats")]
    public float speed; // Movement speed of the enemy
    public float damage; // Damage caused by the enemy

    // Patrolling
    [Header("Patrolling")]
    public Transform[] patrolPoints; // Array of patrol points
    private int currentPatrolIndex; // Index of the current patrol point

    // Attacking
    [Header("Attacking")]
    public float timeBetweenAttacks; // Time between attacks
    private bool alreadyAttacked; // Flag indicating if the enemy has already attacked
    public GameObject hitBox; // Hit box game object for attacking
    public float distanceFromPlayer; // Distance from the player
    private float lastAttackTime; // Time of the last attack

    // States
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
        // Check for sight and attack range
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
        Vector3 targetPosition = player.position - (transform.forward * distanceFromPlayer); // Calculate the target position to chase the player
        agent.SetDestination(targetPosition); // Set the destination to the target position
    }

    private void AttackPlayer()
    {
        Vector3 targetPosition = player.position - (transform.forward * distanceFromPlayer); // Calculate the target position to attack the player
        agent.SetDestination(targetPosition); // Set the destination to the target position

        transform.LookAt(player); // Make the enemy face the player

        if (!alreadyAttacked && Time.time - lastAttackTime >= timeBetweenAttacks)
        {
            // Attack code here
            hitBox.SetActive(true); // Activate the hit box for attacking
            // End of attack code

            alreadyAttacked = true; // Set alreadyAttacked flag to true
            lastAttackTime = Time.time; // Update the last attack time

            Invoke(nameof(ResetAttack), timeBetweenAttacks); // Call ResetAttack method after the specified time
        }
    }

    private void ResetAttack()
    {
        hitBox.SetActive(false); // Deactivate the hit box
        alreadyAttacked = false; // Reset the alreadyAttacked flag
        lastAttackTime = Time.time; // Update the last attack time
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange); // Draw a wire sphere representing the attack range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange); // Draw a wire sphere representing the sight range
    }
}
using UnityEngine;
using UnityEngine.AI;

public class SniperEnemy : MonoBehaviour
{
    [Header("References")]
    public NavMeshAgent agent; // Reference to the NavMeshAgent component
    public Transform player; // Reference to the player's transform
    public PlayerController playerController; // Reference to the player's controller script

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
    bool alreadyAttacked; // Flag indicating if the enemy has already attacked
    public float attackTimer; // Timer for attacks
    public float normalAttackTimer; // Normal attack timer
    public Transform sniperAttackPoint; // Attack point for the sniper enemy
    public bool canSee; // Flag indicating if the enemy has a line of sight to the player

    // States
    [Header("States")]
    public float sightRange; // Range for detecting the player
    public float attackRange; // Range for attacking the player
    public bool playerInSightRange, playerInAttackRange; // Flags indicating if the player is within sight range and attack range

    private LineRenderer lineRenderer; // Reference to the LineRenderer component

    private void Awake()
    {
        player = GameObject.Find("Player").transform; // Find and assign the player's transform
        agent = GetComponent<NavMeshAgent>(); // Get the NavMeshAgent component of the enemy
        agent.speed = speed; // Set the movement speed of the enemy

        lineRenderer = GetComponent<LineRenderer>(); // Get the LineRenderer component
        if (lineRenderer != null)
        {
            lineRenderer.positionCount = 2; // Set the number of positions for the line renderer
        }
        else
        {
            Debug.LogError("Line Renderer component not found!");
        }
    }

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>(); // Find and assign the player's controller script
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

        if (playerInAttackRange)
        {
            // Set positions for the line renderer
            if (lineRenderer != null && canSee)
            {
                lineRenderer.enabled = true;
                lineRenderer.SetPosition(0, sniperAttackPoint.position);
                lineRenderer.SetPosition(1, player.position);
            }
            else
            {
                lineRenderer.enabled = false;
            }

            // Perform raycast
            RaycastHit hit;
            Vector3 direction = player.position - sniperAttackPoint.position;
            if (Physics.Raycast(sniperAttackPoint.position, direction, out hit, attackRange))
            {
                // Check if the raycast hits the player
                if (hit.collider.CompareTag("Player") || hit.collider.CompareTag("Collectable"))
                {
                    // Player is hit, do something
                    canSee = true;
                }
                else
                {
                    // There is an obstacle between the player and sniper, do something else
                    canSee = false;
                }
            }
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

    // Patrolling between points
    private void Patroling()
    {
        if (patrolPoints.Length == 0)
        {
            Debug.LogWarning("No patrol points assigned!");
            return;
        }

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            // Reached the current patrol point, move to the next one
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }
    }

    // Chasing the player
    private void ChasePlayer()
    {
        agent.SetDestination(player.position); // Set the destination to the player's position
    }

    // Attacking the player
    private void AttackPlayer()
    {
        // Make sure the enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player); // Rotate to face the player

        if (!alreadyAttacked && canSee)
        {
            attackTimer -= Time.deltaTime;

            if (attackTimer <= 0)
            {
                playerController.TakeDamage(damage); // Call the TakeDamage method of the player controller to damage the player
                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
                attackTimer = normalAttackTimer;
            }
        }
        else
        {
            attackTimer = normalAttackTimer;
        }
    }

    // Reset the attack flag
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange); // Draw a wire sphere representing the attack range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange); // Draw a wire sphere representing the sight range
    }
}
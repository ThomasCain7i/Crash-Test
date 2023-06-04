using UnityEngine;
using UnityEngine.AI;

public class SniperEnemy : MonoBehaviour
{
    [Header("References")]
    public NavMeshAgent agent;
    public Transform player;
    public PlayerController playerController;

    public LayerMask whatIsGround, whatIsPlayer;

    [Header("Stats")]
    public float speed;
    public float damage;

    //Patrolling
    [Header("Patrolling")]
    public Transform[] patrolPoints; // Array of patrol points
    private int currentPatrolIndex; // Index of the current patrol point

    //Attacking
    [Header("Attacking")]
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public float attackTimer;
    public float normalAttackTimer;
    public Transform sniperAttackPoint;
    public bool canSee;

    //States
    [Header("States")]
    public float sightRange;
    public float attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private LineRenderer lineRenderer;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;

        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer != null)
        {
            lineRenderer.positionCount = 2;
        }
        else
        {
            Debug.LogError("Line Renderer component not found!");
        }
    }

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange)
            Patroling();
        else if (playerInSightRange && !playerInAttackRange)
            ChasePlayer();
        else if (playerInAttackRange && playerInSightRange)
            AttackPlayer();

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

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked && canSee)
        {
            attackTimer -= Time.deltaTime;

            if (attackTimer <= 0)
            {
                playerController.TakeDamage(damage);
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

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
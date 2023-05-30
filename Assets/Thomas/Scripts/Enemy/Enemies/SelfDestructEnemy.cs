using UnityEngine;
using UnityEngine.AI;

public class SelfDestructEnemy : MonoBehaviour
{
    [Header("References")]
    public NavMeshAgent agent;
    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    [Header("Stats")]
    public float speed;
    public float damage;

    // Patrolling
    [Header("Patrolling")]
    public Transform[] patrolPoints; // Array of patrol points
    private int currentPatrolIndex; // Index of the current patrol point

    // Attacking
    [Header("Attacking")]
    public GameObject explosionPrefab;
    public Transform targetTransform; // Reference to the target transform
    public float distanceFromPlayer;
    [SerializeField]
    private float countdownTimer;
    [SerializeField]
    private bool countdownBool = false;

    // States
    [Header("States")]
    public float sightRange;
    public float attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
    }

    private void Update()
    {
        // Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange)
            Patroling();
        else if (playerInSightRange && !playerInAttackRange)
            ChasePlayer();
        else if (playerInAttackRange && playerInSightRange)
            AttackPlayer();

        if (countdownTimer > 0)
        {
            countdownTimer -= Time.deltaTime;

            if (countdownTimer < 0)
            {
                Explode();
            }
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
        Vector3 targetPosition = player.position - (transform.forward * distanceFromPlayer);
        agent.SetDestination(targetPosition);

        if (countdownBool == false)
        {
            Countdown();
        }
    }

    private void AttackPlayer()
    {
        Vector3 targetPosition = player.position - (transform.forward * distanceFromPlayer);
        agent.SetDestination(targetPosition);

        transform.LookAt(player);
    }

    void Countdown()
    {
        countdownBool = true;
        countdownTimer = 3f;
    }

    void Explode()
    {
        // Spawn the sphere at the target transform's position
        Instantiate(explosionPrefab, targetTransform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
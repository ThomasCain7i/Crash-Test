using System.Collections;
using UnityEngine;

public class SquashTrap : MonoBehaviour
{
    public Transform startPosition; // Starting position of the wall
    public Transform endPosition; // Ending position of the wall

    public float smashSpeed = 3f; // Speed at which the wall moves
    public float returnSpeed = 1f; // Speed at which the wall moves

    public float delay = 2f; // Delay in seconds before moving back to the start position
    public float startDelay; // Delay in seconds before moving back to the start position
    public float smashingDuration = 2f; // Duration of the smashing animation

    [SerializeField]
    private bool isMoving = false; // Flag to check if the wall is currently moving
    [SerializeField]
    private bool isReturning = false; // Flag to check if the wall is currently moving

    private PlayerController playerController;
    private float damage = 5f;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        StartMovement();
    }

    private void Update()
    {
        startDelay -= Time.deltaTime;

        if (isMoving && startDelay <= 0)
        {
            if (!isReturning)
            {
                float step = smashSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, endPosition.position, step);

                if (Vector3.Distance(transform.position, endPosition.position) < 0.1f)
                {
                    StartCoroutine(WaitAndReturn());
                }
            }
            else
            {
                float step = returnSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, startPosition.position, step);

                if (Vector3.Distance(transform.position, startPosition.position) < 0.1f)
                {
                    StartCoroutine(WaitAndSmash());
                }
            }
        }
    }

    private IEnumerator WaitAndReturn()
    {
        yield return new WaitForSeconds(delay);
        isReturning = true;
    }

    private IEnumerator WaitAndSmash()
    {
        yield return new WaitForSeconds(smashingDuration);
        isReturning = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isReturning) 
        {
            Debug.Log("Player touched closing walls");
            playerController.hitTimer = 0;
            playerController.Armour = 0;
            playerController.TakeDamage(damage);

            StopMovement();
            StartCoroutine(ResetMovement());
        }
    }

    private void StartMovement()
    {
        isMoving = true;
    }

    private void StopMovement()
    {
        isMoving = false;
    }

    private IEnumerator ResetMovement()
    {
        yield return new WaitForSeconds(delay);
        StartMovement();
    }
}

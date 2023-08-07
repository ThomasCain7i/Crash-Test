using System.Collections;
using UnityEngine;

public class ClosingWalls : MonoBehaviour
{
    public Transform startPosition; // Starting position of the wall
    public Transform endPosition; // Ending position of the wall
    public float movementSpeed = 0.2f; // Speed at which the wall moves
    public float movementSpeedNormal = 0.2f; // Speed at which the wall moves
    public float delay = 2f; // Delay in seconds before moving back to the start position

    private bool isMoving = false; // Flag to check if the wall is currently moving
    private bool movingTowardsPlayer = true; // Flag to indicate the movement direction

    private PlayerController playerController;

    private float damage = 5;
    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if (isMoving)
        {
            // Calculate the movement step based on the speed and time
            float step = movementSpeed * Time.deltaTime;

            if (movingTowardsPlayer)
            {
                movementSpeed = 0.2f;
                // Move the wall towards the end position using Lerp
                transform.position = Vector3.Lerp(transform.position, endPosition.position, step);

                // Check if the wall has reached the end position
                if (Vector3.Distance(transform.position, endPosition.position) < 1f)
                {
                    // Wait for a delay before moving back to the start position
                    StartCoroutine(MoveBackWithDelay());
                }
            }
            else
            {
                movementSpeed = 10;
                // Move the wall towards the start position using Lerp
                transform.position = Vector3.Lerp(transform.position, startPosition.position, step);

                // Check if the wall has reached the start position
                if (Vector3.Distance(transform.position, startPosition.position) < 0.1f)
                {
                    // Reset the movement direction and stop the movement
                    movingTowardsPlayer = true;
                    isMoving = false;
                }
            }
        }
    }

    private IEnumerator MoveBackWithDelay()
    {
        // Wait for the specified delay before moving back to the start position
        yield return new WaitForSeconds(delay);

        // Reverse the movement direction and start moving back
        movingTowardsPlayer = false;

        // Start the movement
        isMoving = true;
    }

    public void StartMovement()
    {
        // Set the wall's initial position to the starting position
        transform.position = startPosition.position;

        // Start the movement
        isMoving = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the wall collides with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            playerController.hitTimer = 0;
            Debug.Log("Player touched closing walls");
            playerController.Armour = 0;
            playerController.TakeDamage(damage);

            // Reverse the movement direction and start moving back
            movingTowardsPlayer = false;

            // Stop the movement temporarily
            isMoving = false;

            // Move back to the start position after a short delay
            StartCoroutine(MoveBackWithDelay());
        }
    }
}

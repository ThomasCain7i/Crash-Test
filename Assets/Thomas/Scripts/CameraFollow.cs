using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float cameraSpeed = 5f;
    public GameObject player;
    public Transform[] pathWaypoints;

    private int currentWaypointIndex = 0;
    private Vector3 offset;

    private void Start()
    {
        if (player != null)
        {
            offset = transform.position - player.transform.position;
        }
    }

    private void Update()
    {
        if (currentWaypointIndex < pathWaypoints.Length)
        {
            Vector3 targetPosition = pathWaypoints[currentWaypointIndex].position;
            Vector3 direction = targetPosition - transform.position;
            direction.y = 0f;

            float distanceToTarget = Vector3.Distance(transform.position, targetPosition);
            float step = cameraSpeed * Time.deltaTime;

            // Move camera towards the target waypoint
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

            // Check if the camera is close enough to the target waypoint
            if (distanceToTarget < 0.1f)
            {
                currentWaypointIndex++;
            }
        }
    }

    private void LateUpdate()
    {
        if (player != null)
        {
            transform.position = player.transform.position + offset;
        }
    }
}
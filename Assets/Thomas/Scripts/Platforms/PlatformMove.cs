using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    [SerializeField]
    private WaypointPath waypointPath;

    [SerializeField]
    private float speed;

    private int targetWaypointIndex;

    private Transform previousWaypoint; 
    private Transform targetWaypoint;


    private float timeToWaypoint;
    private float timeElapsed;

    void Start()
    {
        TargetNextWaypoint();
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;

        float elapsedPercentage = timeElapsed / timeToWaypoint;
        transform.position = Vector3.Lerp(previousWaypoint.position, targetWaypoint.position, elapsedPercentage);

        if(elapsedPercentage >= 1)
        {
            TargetNextWaypoint();
        }
    }

    private void TargetNextWaypoint()
    {
        previousWaypoint = waypointPath.GetWaypoint(targetWaypointIndex);

        targetWaypointIndex = waypointPath.GetNextPointIndex(targetWaypointIndex);
        targetWaypoint = waypointPath.GetWaypoint(targetWaypointIndex);

        timeElapsed = 0;

        float distanceToWaypoint = Vector3.Distance(previousWaypoint.position, targetWaypoint.position);

        timeToWaypoint = distanceToWaypoint / speed;
    }
}

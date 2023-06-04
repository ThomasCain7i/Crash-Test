using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // What to follow
    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    // After everything else is moved.
    private void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Unity handles looking rotation
        transform.LookAt(target);
    }
}
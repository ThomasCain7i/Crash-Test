using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public bool threeD = true;
    public bool twoD = false;
    // What to follow
    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 threeOffset;
    public Vector3 twoOffset;

    // After everything else is moved.
    private void FixedUpdate()
    {
        if(threeD)
        {
            Vector3 desiredPosition = target.position + threeOffset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            // Unity handles looking rotation
            transform.LookAt(target);
        }

        if(twoD)
        {
            Vector3 desiredPosition = target.position + twoOffset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            // Unity handles looking rotation
            transform.LookAt(target);
        }
    }
}
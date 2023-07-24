using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public bool oneD = true;
    public bool twoD = false;
    public bool threeD = false;
    public bool fourD = false;
    public bool alanis = false;
    public bool maze = false;

    // What to follow
    public Transform target;
    public Transform target2;
    public Transform target3;

    public float smoothSpeed = 0.125f;
    public Vector3 oneOffset;
    public Vector3 twoOffset;
    public Vector3 threeOffset;
    public Vector3 forthOffset;
    public Vector3 alanisOffset;
    public Vector3 mazeOffset;

    // After everything else is moved.
    private void FixedUpdate()
    {
        if(oneD)
        {
            Vector3 desiredPosition = target.position + oneOffset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            // Unity handles looking rotation
            transform.LookAt(target);
        }

        if(twoD)
        {
            Vector3 desiredPosition = target2.position + twoOffset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            // Unity handles looking rotation
            transform.LookAt(target2);
        }

        if (threeD)
        {
            Vector3 desiredPosition = target2.position + threeOffset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            // Unity handles looking rotation
            transform.LookAt(target2);
        }

        if (fourD)
        {
            Vector3 desiredPosition = target2.position + forthOffset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            // Unity handles looking rotation
            transform.LookAt(target2);
        }

        if (alanis)
        {
            Vector3 desiredPosition = target3.position + alanisOffset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            // Unity handles looking rotation
            transform.LookAt(target3);
        }

        if (maze)
        {
            Vector3 desiredPosition = target.position + mazeOffset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            // Unity handles looking rotation
            transform.LookAt(target);
        }
    }
}
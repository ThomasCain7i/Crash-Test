using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    private CameraFollow cameraFollow;

    [SerializeField]
    private bool two, three, happend;

    private void Start()
    {
        cameraFollow = FindObjectOfType<CameraFollow>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (two && !happend)
        {
            cameraFollow.twoD = true;
            cameraFollow.threeD = false;
            happend = true;
        }

        if(three)
        {
            cameraFollow.twoD = false;
            cameraFollow.threeD = true;
        }
    }
}

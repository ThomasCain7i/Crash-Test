using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    private CameraFollow cameraFollow;

    [SerializeField]
    private bool one, two, three, happend;
    [SerializeField]
    private bool countDownStart = false, mazeIN, mazeOUT;

    [SerializeField]
    private float countDown, countDown2, countDown3;

    private void Start()
    {
        cameraFollow = FindObjectOfType<CameraFollow>();
    }

    private void Update()
    {
        if (countDownStart == true)
        {
            countDown -= Time.deltaTime;
            countDown2 -= Time.deltaTime;
            countDown3 -= Time.deltaTime;

            if (countDown < 0 && two)
            {
                cameraFollow.oneD = false;
                cameraFollow.twoD = false;
                cameraFollow.threeD = true;
                cameraFollow.fourD = false;
            }

            if (countDown2 < 0 && two)
            {
                cameraFollow.oneD = false;
                cameraFollow.twoD = false;
                cameraFollow.threeD = false;
                cameraFollow.fourD = true;
            }

            if (countDown3 < 0)
            {
                cameraFollow.oneD = true;
                cameraFollow.twoD = false;
                cameraFollow.threeD = false;
                cameraFollow.fourD = false;
                countDownStart = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!mazeIN && !mazeOUT)
        {
            countDownStart = true;
        }

        if (one)
        {
            cameraFollow.oneD = true;
            cameraFollow.twoD = false;
            cameraFollow.threeD = false;
            cameraFollow.fourD = false;
            countDownStart = false;
        }

        if (two && !happend)
        {
            cameraFollow.oneD = false;
            cameraFollow.twoD = true;
            cameraFollow.threeD = false;
            cameraFollow.fourD = false;
            happend = true;
        }

        if (mazeIN)
        {
            cameraFollow.oneD = false;
            cameraFollow.twoD = false;
            cameraFollow.threeD = false;
            cameraFollow.fourD = false;
            cameraFollow.maze = true;
        }

        if (mazeOUT)
        {
            cameraFollow.oneD = true;
            cameraFollow.twoD = false;
            cameraFollow.threeD = false;
            cameraFollow.fourD = false;
            cameraFollow.maze = false;
        }
    }
}

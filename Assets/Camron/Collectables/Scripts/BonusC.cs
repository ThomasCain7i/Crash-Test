using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusC : MonoBehaviour
{
    // Start is called before the first frame update
    public int rotateSpeed;

    public bool sand;
    public bool snow;
    public bool fire;
    public bool water;

    void Start()
    {
        rotateSpeed = 2;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0, rotateSpeed, 0, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("dog got bone");
        if (other.tag == "Player")
        {
            PlayerControllerCam playerController = other.GetComponent<PlayerControllerCam>();

            if (playerController != null && water == true)
            {
                playerController.WaterCollectedBonus();
            }

            if (playerController != null && fire == true)
            {
                playerController.FireCollectedBonus();
            }

            if (playerController != null && snow == true)
            {
                playerController.SnowCollectedBonus();
            }

            if (playerController != null && sand == true)
            {
                playerController.SandCollectedBonus();
            }

            Destroy(gameObject);
        }
    }
}

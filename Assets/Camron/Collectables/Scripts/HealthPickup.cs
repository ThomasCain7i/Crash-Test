using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    // Start is called before the first frame update
    public int rotateSpeed;
    [SerializeField]
    private PlayerController playerController;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        rotateSpeed = 2;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0, rotateSpeed, 0, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (playerController != null)
            {
                playerController.GainHealth();
                Debug.Log("Player picked up the health");
            }
            Destroy(gameObject);
        }
    }
}

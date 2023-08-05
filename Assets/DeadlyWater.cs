using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadlyWater : MonoBehaviour
{
    public PlayerController playerController;

    [SerializeField]
    private float damage = 10;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerController.TakeDamage(damage);
        }
        if(other.tag == "hbi")
        {
            playerController.currentHealth = 0; 
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFloor : MonoBehaviour
{
    public float damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player touched the death floor");
            other.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
        }
    }
}

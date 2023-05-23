using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideArrowTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject arrowShooter;

    public CollideArrowShooter collideArrowShooter;

    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player hit by spikes");
        if (other.tag == "Player")
        {
            collideArrowShooter.Shoot();
        }
    }
}
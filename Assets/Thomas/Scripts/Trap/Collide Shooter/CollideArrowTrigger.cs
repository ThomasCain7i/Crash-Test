using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideArrowTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject arrowShooter, arrowShooter2, arrowShooter3;

    public CollideArrowShooter collideArrowShooter, collideArrowShooter2, collideArrowShooter3;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player toched colide plate");
        if (other.tag == "Player")
        {
            if(collideArrowShooter != null)
            collideArrowShooter.Shoot();
            if(collideArrowShooter2 != null)
            collideArrowShooter2.Shoot();
            if(collideArrowShooter3 != null)
            collideArrowShooter3.Shoot();
        }
    }
}
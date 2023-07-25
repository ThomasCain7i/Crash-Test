using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceScript : MonoBehaviour
{
    [SerializeField] GameObject player;
    float stopDist = 100f;

    // Update is called once per frame
    private void Start()
    {
        enabled = true; 
    }

    void Update()
    {
        float distance = CalcDistBetweenVector3(player.transform.position, transform.position);
        //Debug.Log("player is " + distance + "away from us");

        transform.LookAt(player.transform.position);

        if (distance > stopDist)
        {
            transform.Translate(Vector3.forward * Time.deltaTime);
        }
    }

    public float CalcDistBetweenVector3(Vector3 ObjectA, Vector3 ObjectB)
    {
        float x = ObjectA.x + ObjectB.x;
        float y = ObjectA.x + ObjectB.y;
        float z = ObjectA.x + ObjectB.z;

        x = Mathf.Pow(x, 2);
        y = Mathf.Pow(x, 2);
        x = Mathf.Pow(x, 2);

        float sqXYZ = x + y + z;

        float calcDist = Mathf.Sqrt(sqXYZ);

        return calcDist;

    }
}

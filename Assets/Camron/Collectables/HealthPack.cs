using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    // Start is called before the first frame update
    public int rotateSpeed;
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
        Debug.Log("collect HealthPack");
        if(other.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}

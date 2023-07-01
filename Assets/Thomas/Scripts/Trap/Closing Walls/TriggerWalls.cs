using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWalls : MonoBehaviour
{
    public GameObject wall1; // Reference to Wall1 GameObject
    public GameObject wall2; // Reference to Wall2 GameObject
    private ClosingWalls closingWalls1; // Reference to the script controlling the movement of Wall1
    private ClosingWalls closingWalls2; // Reference to the script controlling the movement of Wall2

    private void Start()
    {
        closingWalls1 = wall1.GetComponent<ClosingWalls>();
        closingWalls2 = wall2.GetComponent<ClosingWalls>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player activated the walls");
            closingWalls1.StartMovement();
            closingWalls2.StartMovement();
        }
    }
}
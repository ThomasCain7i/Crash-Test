using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTrial : MonoBehaviour
{
    
    
    public GameObject mainCamera;
    public GameObject secondCamera; 

    public Transform player;
    public Transform otherEnd;
    public Transform camera3D;
    public Transform camera2;

    public Vector3 threeOffset;
    public float smoothSpeed = 0.125f;
    private bool mode2D;

    public PlayerController playerScript; 
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (mode2D == true)
        {
            
            secondCamera.SetActive(true);
            mainCamera.SetActive(false);


            player.transform.position = otherEnd.transform.position;


        }
        else if(mode2D == false)
        {
            secondCamera.SetActive(false);
            mainCamera.SetActive(true);
        }
    }

   
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            mode2D = true;
            
        } 
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            mode2D = false;
            
        }

    }



}

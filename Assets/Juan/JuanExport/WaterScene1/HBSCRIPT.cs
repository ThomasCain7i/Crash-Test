using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//JUAN CODE
public class HBSCRIPT : MonoBehaviour
{
    Rigidbody _hoverBoard;

    public float multiplier;
    public float moveForce;
    public float turnTorque;

    public Transform[] anchors = new Transform[4];
    RaycastHit[] hits = new RaycastHit[4];

    public Transform player;
    public Transform ridePoint;


    public PlayerControllerJuan playerScript;
    public Animator playerAnimator; 




    // Start is called before the first frame update
    void Start()
    {
        _hoverBoard = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < 4; i++)
            ApplyForce(anchors[i], hits[i]);
        _hoverBoard.AddForce(Input.GetAxis("Vertical") * moveForce * transform.forward);
        _hoverBoard.AddTorque(Input.GetAxis("Horizontal") * turnTorque * transform.up);
    }

    void ApplyForce( Transform anchor, RaycastHit hit)
    {
        if(Physics.Raycast(anchor.position, -anchor.up, out hit))
        {
            float force = 0;
            force = Mathf.Abs(1 / (hit.point.y - anchor.position.y));
            _hoverBoard.AddForceAtPosition(transform.up * force * multiplier, anchor.position, ForceMode.Acceleration);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Riding");
            playerAnimator.SetBool("IsRiding", true);
            player.transform.position = ridePoint.transform.position;
            player.transform.rotation = ridePoint.transform.rotation;
            multiplier = 3f;
            moveForce = 6000f;
            turnTorque = 500f;
            playerScript.moveSpeed = 0;
            playerScript.rotationSpeed = 0;

        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        Debug.Log("Riding");
    //        other.transform.SetParent(null);
    //    }
    //}

    //public void MovingMechanic()
    //{

    //}
}

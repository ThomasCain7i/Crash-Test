using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    Rigidbody rb;
    public Vector3 boxSize;
    public float maxDistance;

    public float jumpForce;
    public float doubleJumpForce;

    public bool isOnGround;
    private bool doubleJump;
    public Animator myAnimator;
   

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isOnGround = true;

        jumpForce = 400f;
        doubleJumpForce = 400f;

        if (isOnGround == true)
        {
            myAnimator.SetBool("Jumping", false);
        }
        else if (isOnGround == false)
        {
            myAnimator.SetBool("Jumping", true);
        }

        if (doubleJump == false)
        {
            myAnimator.SetBool("Jumping", false);
        }
        else if (doubleJump == true)
        {
            myAnimator.SetBool("Jumping", true);
        }
        
    }

    // Update is called once per frame
    void Update()
    {


        if (isOnGround == true)
        {
            myAnimator.SetBool("Jumping", false);
            if (Input.GetKeyDown(KeyCode.Space))
            {


                myAnimator.SetBool("Jumping", true);
                rb.AddForce(new Vector2(0f, jumpForce));


                isOnGround = false;
                doubleJump = true;
            }
           

            
        }
        else
        {
            
            if (doubleJump && Input.GetKeyDown(KeyCode.Space))
            {
                myAnimator.SetBool("Jumping", true);
                rb.AddForce(new Vector2(0f, doubleJumpForce));
                doubleJump = false;
            }
        }

    }

    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isOnGround = true;
            doubleJump = false;

        }
    }
}

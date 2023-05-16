using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public float jumpForce = 5f;
    public int maxJumps = 2;

    private Rigidbody rb;
    private int jumpsLeft;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        jumpsLeft = maxJumps;
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calculate movement and rotation vectors
        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical) * moveSpeed * Time.fixedDeltaTime;
        Quaternion rotation = Quaternion.Euler(0f, moveHorizontal * rotationSpeed * Time.fixedDeltaTime, 0f);

        // Apply movement and rotation to the rigidbody
        rb.MovePosition(transform.position + movement);
        rb.MoveRotation(rb.rotation * rotation);

        if (Input.GetButtonDown("Jump") && jumpsLeft > 0)
        {
            // Jump
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpsLeft--;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Reset jumps if player lands on the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpsLeft = maxJumps;
        }
    }
}
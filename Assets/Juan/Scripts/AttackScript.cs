using UnityEngine;

public class AttackScript : MonoBehaviour
{
    // Elemental Buffs
    [Header("Elemental Buffs")]
    public bool fire = false;    // Indicates if the fire buff is active
    public bool snow = false;    // Indicates if the snow buff is active
    public bool sand = false;    // Indicates if the sand buff is active
    public bool water = false;   // Indicates if the water buff is active

    // Elemental Prefabs
    [Header("Elemental Prefabs")]
    public GameObject firePrefab;    // Prefab for the fire attack
    public GameObject snowPrefab;    // Prefab for the snow attack
    public GameObject sandPrefab;    // Prefab for the sand attack
    public GameObject waterPrefab;   // Prefab for the water attack

    // Bark Stuff (Point of Spawn, Speed, etc.)
    [Header("Bark")]
    public Transform barkSpawn;      // The spawn point for the bark attack
    public GameObject barkPrefab;    // Prefab for the bark attack
    public float barkSpeed = 5f;     // The speed at which the bark attack travels

    // Cooldown
    [Header("Bark Cooldowns")]
    public float coolDownTime;       // The cooldown time between bark attacks
    public float nextBarkTime;       // The time when the next bark attack can occur

    // Rigidbody
    private Rigidbody rb;            // The Rigidbody component of the character
    private bool isGrounded;         // Indicates if the character is grounded

    // Smash
    [Header("Smash")]
    public float smashForce;         // The force applied for the smash attack
    public Transform smashPoint;   // The point where the impulse object spawns
    public GameObject smashObject; // The object spawned for the smash attack
    private bool smashing = false;

    public Animator animator;        // The Animator component for controlling animations

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();   // Get the Rigidbody component from the character
        animator = GetComponent<Animator>();  // Get the Animator component from the character
    }

    // Update is called once per frame
    void Update()
    {
        // Check elemental buffs and assign appropriate prefab
        if (fire == true)
        {
            barkPrefab = firePrefab;   // Assign the fire prefab to the bark prefab
        }
        // Add other elemental buffs and assignments here

        // Check if it's time for the next bark attack
        if (Time.time > nextBarkTime)
        {
            // Bark Attack - Juan
            if (Input.GetKeyDown(KeyCode.R))
            {
                animator.SetBool("IsBarking", true);  // Set the "IsBarking" parameter in the animator to true
                // Spawn the bark attack from the specific point off the player
                var bark = Instantiate(barkPrefab, barkSpawn.position, barkSpawn.rotation);
                // Apply force and speed to the bark
                bark.GetComponent<Rigidbody>().velocity = barkSpawn.forward * barkSpeed;

                // Cooldown for the bark attack - Juan
                nextBarkTime = Time.time + coolDownTime;   // Set the time when the next bark attack can occur
            }
            else
            {
                animator.SetBool("IsBarking", false); // Set the "IsBarking" parameter in the animator to false
            }
        }

        // Smash Attack - Juan
        // Check if the player is not grounded and perform the smash attack
        if (isGrounded == false)
        {
            if (Input.GetKeyDown(KeyCode.E) && isGrounded == false)
            {
                smashing = true;
                // Apply downward force to perform the smash attack
                rb.AddForce(Vector3.down * smashForce, ForceMode.VelocityChange);
            }
        }
    }

    // Detect collision with the ground
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && smashing == true)
        {
            // Spawn the impulse object at the specified point
            var impulse = Instantiate(smashObject, smashPoint.position, smashPoint.rotation);
        }
    }
}

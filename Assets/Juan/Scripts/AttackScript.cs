// Juan Thomas

using UnityEngine;

public class AttackScript : MonoBehaviour
{
    // Elemental Buffs
    [Header("Elemental Buffs")]
    public int fire = 0;    // Indicates if the fire buff is active
    public int snow = 0;    // Indicates if the snow buff is active
    public int sand = 0;    // Indicates if the sand buff is active
    public int water = 0;   // Indicates if the water buff is active


    // Elemental Prefabs
    [Header("Elemental Prefabs")]
    public GameObject firePrefab;    // Prefab for the fire attack
    public GameObject snowPrefab;    // Prefab for the snow attack
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
    public GameObject smashPrefab; // The object spawned for the smash attack
    public bool smashing = false; // Is the player smashing

    // Smash Upgrade
    public bool platform = false;

    [Header("Animator")]
    public Animator animator;        // The Animator component for controlling animations
    private SoundPlayer soundPlayer;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();   // Get the Rigidbody component from the character
        animator = GetComponent<Animator>();  // Get the Animator component from the character
        soundPlayer = FindObjectOfType<SoundPlayer>();
        gameManager = FindObjectOfType<GameManager>();

        gameManager.LoadSettings();
        Debug.Log("Loaded Attack Script");

    }

    // Update is called once per frame
    void Update()
    {
        // Check elemental buffs and assign appropriate prefab
        if (fire > 0)
        {
            barkPrefab = firePrefab;   // Assign the fire prefab to the bark prefab
        }

        // Check if it's time for the next bark attack
        if (Time.time > nextBarkTime)
        {
            // Bark Attack - Juan
            if (Input.GetKeyDown(KeyCode.R))
            {
                soundPlayer.PlayRanged();
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
        if (isGrounded == false && !smashing)
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
            soundPlayer.PlaySmash();
            // Spawn the impulse object at the specified point
            var impulse = Instantiate(smashPrefab, smashPoint.position, smashPoint.rotation);
            smashing = false;
        }
    }
}

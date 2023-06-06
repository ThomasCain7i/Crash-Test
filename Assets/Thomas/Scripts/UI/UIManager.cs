using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // References
    [Header("References")]
    public PlayerController playerController;

    // Health
    [Header("Health")]
    public Text healthText;
    private float healthTimer;
    [SerializeField]
    private float normalHealthTimer;
    [SerializeField]
    private GameObject healthCanvas;

    // Power Ups
    [Header("Power Ups")]
    public GameObject speedImage;
    public GameObject tripleJumpImage;
    public GameObject slowMoImage;
    [SerializeField]
    private float speedTimer, tripleJumpTimer, slowMoTimer;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Timers
        healthTimer -= Time.deltaTime;
        speedTimer -= Time.deltaTime;
        tripleJumpTimer -= Time.deltaTime;
        slowMoTimer -= Time.deltaTime;

        // Text
        healthText.text = playerController.currentHealth.ToString();

        // Actives
        // HEALTH
        if (healthTimer > 0)
        {
            healthCanvas.SetActive(true);
        }
        else
        {
            healthCanvas.SetActive(false);
        }

        // SPEED
        if (speedTimer > 0)
        {
            speedImage.SetActive(true);
        }
        else
        {
            speedImage.SetActive(false);
        }

        // TRIPLE JUMP
        if (tripleJumpTimer > 0)
        {
            tripleJumpImage.SetActive(true);
        }
        else
        {
            tripleJumpImage.SetActive(false);
        }

        // SLOW MO
        if (slowMoTimer > 0)
        {
            slowMoImage.SetActive(true);
        }
        else
        {
            slowMoImage.SetActive(false);
        }
    }

    public void HealthUI()
    {
        healthTimer = normalHealthTimer;
    }

    public void SpeedUI()
    {
        speedTimer = playerController.normalSpeedTimer;
    }

    public void TripleJumpUI()
    {
        tripleJumpTimer = playerController.normalTripleJumpTimer;
    }

    public void SlowMoUI()
    {
        slowMoTimer = playerController.normalTimeSlowTimer;
    }
}
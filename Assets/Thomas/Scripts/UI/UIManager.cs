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
    [SerializeField]
    private float healthTimer, armourTimer, armourBrokenTimer;
    [SerializeField]
    private float normalHealthTimer;
    [SerializeField]
    private GameObject healthCanvas, armourCanvas, armourBrokenCanvas;

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
        armourTimer -= Time.deltaTime;
        armourBrokenTimer -= Time.deltaTime;

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

        // ARMOUR
        if (armourTimer > 0)
        {
            armourCanvas.SetActive(true);
        }
        else
        {
            armourCanvas.SetActive(false);
        }

        if (armourBrokenTimer > 0)
        {
            armourBrokenCanvas.SetActive(true);
        }
        else
        {
            armourBrokenCanvas.SetActive(false);
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

    public void ArmourUIon()
    {
        armourTimer = normalHealthTimer;
    }

    public void ArmourBrokenUI()
    {
        armourBrokenTimer = normalHealthTimer;
    }

    public void ArmourUIoff()
    {
        armourTimer = 0;
        armourBrokenTimer = 0;
    }
}
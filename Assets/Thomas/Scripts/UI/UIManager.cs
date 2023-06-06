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
    private float healthTimer;
    [SerializeField]
    private float normalHealthTimer;
    [SerializeField]
    private GameObject healthCanvas;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        healthTimer -= Time.deltaTime;

        healthText.text = playerController.currentHealth.ToString();

        if (healthTimer > 0)
        {
            healthCanvas.SetActive(true);
        }
        else
        {
            healthCanvas.SetActive(false);
        }

    }

    public void HealthUpdate()
    {
        healthTimer = normalHealthTimer;
    }
}
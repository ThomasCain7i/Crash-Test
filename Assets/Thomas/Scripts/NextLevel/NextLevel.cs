// Thomas

using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [Header("References")]
    public PlayerController playerController;
    public PlayerControllerCam pc;
    public GameManager gameManager;
    private MenuSoundManager menuSoundManager;
    private AttackScript attackScript;
    public int bonusToCollect = 5;
    public int bonus;
    public int bonusC;

    [Header("Bools")]
    [SerializeField]
    private bool hub;
    [SerializeField]
    private bool water, waterWin, sand, sandWin, snow, snowWin, fire, fireWin, bonusLevel, bonusWin;


    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        pc = FindObjectOfType<PlayerControllerCam>();
        gameManager = FindObjectOfType<GameManager>();
        attackScript = FindObjectOfType<AttackScript>();
        bonus = playerController.BonusCount;

        menuSoundManager = FindObjectOfType<MenuSoundManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        menuSoundManager.PlayLevelComplete();
        gameManager.SaveCollectables();
        gameManager.SaveElements();

        Debug.Log("Saved");

        if (other.tag == "Player")
        {
            if (hub)
            {
                SceneManager.LoadScene(1);
            }

            if (water)
            {
                SceneManager.LoadScene(2);
            }

            if (waterWin && attackScript.water == 1)
            {
                SceneManager.LoadScene(3);
            }

            if (sand)
            {
                SceneManager.LoadScene(4);
            }

            if (sandWin && attackScript.sand == 1)
            {
                SceneManager.LoadScene(5);
            }

            if (snow)
            {
                SceneManager.LoadScene(6);
            }

            if (snowWin && attackScript.snow == 1)
            {
                SceneManager.LoadScene(7);
            }

            if (fire)
            {
                SceneManager.LoadScene(8);
            }

            if (fireWin && attackScript.fire == 1)
            {
                SceneManager.LoadScene(9);
            }

            if (bonusLevel && bonus == bonusToCollect)
            {
                SceneManager.LoadScene(10);
            }

            if (bonusLevel && bonusC == bonusToCollect)
            {
                SceneManager.LoadScene(10);
            }
        }
    }
}
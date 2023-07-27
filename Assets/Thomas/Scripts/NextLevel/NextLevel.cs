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
    private LevelLoader levelLoader;
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
        levelLoader = FindObjectOfType<LevelLoader>();
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
                levelLoader.LoadLevel(1);
            }

            if (water)
            {
                levelLoader.LoadLevel(2);
            }

            if (waterWin && attackScript.water == 1)
            {
                levelLoader.LoadLevel(3);
            }

            if (sand)
            {
                levelLoader.LoadLevel(4);
            }

            if (sandWin && attackScript.sand == 1)
            {
                levelLoader.LoadLevel(5);
            }

            if (snow)
            {
                levelLoader.LoadLevel(6);
            }

            if (snowWin && attackScript.snow == 1)
            {
                levelLoader.LoadLevel(7);
            }

            if (fire)
            {
                levelLoader.LoadLevel(8);
            }

            if (fireWin && attackScript.fire == 1)
            {
                levelLoader.LoadLevel(9);
            }

            if (bonusLevel && bonus == bonusToCollect)
            {
                levelLoader.LoadLevel(10);
            }

            if (bonusLevel && bonusC == bonusToCollect)
            {
                levelLoader.LoadLevel(10);
            }
        }
    }
}
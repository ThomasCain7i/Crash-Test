// Thomas

using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    // save or not
    [SerializeField]
    private bool save;

    [Header("References")]
    public PlayerController playerController;
    public GameManager gameManager;
    private MenuSoundManager menuSoundManager;
    private AttackScript attackScript;
    private LevelLoader levelLoader;
    private BossNextLevel bossNextLevel;
    public int bonusToCollect = 5;
    public int bonus;
    public int bonusC;
    [SerializeField]
    public GameObject gameObject;

    [Header("Bools")]
    [SerializeField]
    private bool menu;
    [SerializeField]
    private bool hub, water, waterWin, sand, sandWin, snow, snowWin, fire, fireWin, boss, ending;


    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        gameManager = FindObjectOfType<GameManager>();
        attackScript = FindObjectOfType<AttackScript>();
        levelLoader = FindObjectOfType<LevelLoader>();
        bossNextLevel = FindObjectOfType<BossNextLevel>();
        bonus = playerController.BonusCount;

        menuSoundManager = FindObjectOfType<MenuSoundManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        menuSoundManager.PlayLevelComplete();

        if(save)
        {
            gameManager.SaveCollectables();
            gameManager.SaveElements();

            Debug.Log("Saved");
        }


        if (other.tag == "Player")
        {
            if (menu)
            {
                Debug.Log("Menu");
                levelLoader.LoadLevel(0);
            }

            if (hub)
            {
                Debug.Log("Hub");
                levelLoader.LoadLevel(3);
            }

            if (water)
            {
                Debug.Log("Water");
                levelLoader.LoadLevel(4);
            }

            if (waterWin && attackScript.water == 1)
            {
                Debug.Log("WaterWin");
                levelLoader.LoadLevel(5);
            }

            if (sand)
            {
                Debug.Log("Sand");
                levelLoader.LoadLevel(6);
            }

            if (sandWin && attackScript.sand == 1)
            {
                Debug.Log("SandWin");
                levelLoader.LoadLevel(7);
            }

            if (snow)
            {
                Debug.Log("SnowWin");
                levelLoader.LoadLevel(8);
            }

            if (snowWin && attackScript.snow == 1)
            {
                Debug.Log("SnowWin");
                levelLoader.LoadLevel(9);
            }

            if (fire)
            {
                Debug.Log("Fire");
                levelLoader.LoadLevel(10);
            }

            if (fireWin && attackScript.fire == 1)
            {
                Debug.Log("FireWin");
                levelLoader.LoadLevel(11);
            }

            if (boss && attackScript.fire == 1 && attackScript.snow == 1 && attackScript.water == 1 && attackScript.fire == 1)
            {
                levelLoader.LoadLevel(12);
            }

            if (boss && attackScript.fire == 0 || attackScript.water == 0 || attackScript.sand == 0 || attackScript.snow == 0)
            {
                gameObject.SetActive(true);
                bossNextLevel.Paused();
            }

            if (ending)
            {
                levelLoader.LoadLevel(13);
            }
           
        }
    }
}
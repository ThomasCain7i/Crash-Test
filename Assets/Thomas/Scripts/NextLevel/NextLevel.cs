// Thomas

using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [Header("References")]
    public PlayerController playerController;
    public GameManager gameManager;
    private SoundManager soundManager;
    public int bonusToCollect = 5;
    public int bonus;

    [Header("Bools")]
    [SerializeField]
    private bool hub;
    [SerializeField]
    private bool water, waterWin, sand, sandWin, snow, snowWin, fire, fireWin, bonusLevel, bonusWin;


    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        gameManager = FindObjectOfType<GameManager>();
        bonus = playerController.BonusCount;
        soundManager = FindObjectOfType<SoundManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        soundManager.PlayLevelComplete();
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

            if (waterWin)
            {
                SceneManager.LoadScene(3);
            }

            if (sand)
            {
                SceneManager.LoadScene(4);
            }

            if (sandWin)
            {
                SceneManager.LoadScene(5);
            }

            if (snow)
            {
                SceneManager.LoadScene(6);
            }

            if (snowWin)
            {
                SceneManager.LoadScene(7);
            }

            if (fire)
            {
                SceneManager.LoadScene(8);
            }

            if (fireWin)
            {
                SceneManager.LoadScene(9);
            }

            if (bonusLevel && bonus == bonusToCollect)
            {
                SceneManager.LoadScene(10);
            }
        }
    }
}
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIWin : MonoBehaviour
{
    public GameManager gameManager;
    public LevelLoader levelLoader;
    [SerializeField]
    private bool fire, water, snow, sand;

    public void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        levelLoader = FindObjectOfType<LevelLoader>();
    }

    public void ContinueGame()
    {
        Time.timeScale = 1f;
        //gameManager.LoadSettings();
        SceneManager.LoadScene(1);
    }

    public void RetryGame()
    {
        if (water)
        {
            Time.timeScale = 1f;
            levelLoader.LoadLevel(2);
        }

        if (sand)
        {
            Time.timeScale = 1f;
            levelLoader.LoadLevel(4);
        }

        if (snow)
        {
            Time.timeScale = 1f;
            levelLoader.LoadLevel(6);
        }

        if (fire)
        {
            Time.timeScale = 1f;
            levelLoader.LoadLevel(8);
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class UIWin : MonoBehaviour
{
    [SerializeField]
    private GameObject gameObject;

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
            gameObject.SetActive(false);
        }

        if (sand)
        {
            Time.timeScale = 1f;
            levelLoader.LoadLevel(4);
            gameObject.SetActive(false);
        }

        if (snow)
        {
            Time.timeScale = 1f;
            levelLoader.LoadLevel(6);
            gameObject.SetActive(false);
        }

        if (fire)
        {
            Time.timeScale = 1f;
            levelLoader.LoadLevel(8);
            gameObject.SetActive(false);
        }
    }
}

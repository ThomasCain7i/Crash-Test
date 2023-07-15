using UnityEngine;
using UnityEngine.SceneManagement;

public class UIWin : MonoBehaviour
{
    public GameManager gameManager;

    public void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void ContinueGame()
    {
        gameManager.LoadSettings();
        SceneManager.LoadScene(1);
    }
}

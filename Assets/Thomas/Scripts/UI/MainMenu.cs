// Thomas

using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject optionsMenu, mainMenu, playMenu, creditsMenu;
    public GameManager gameManager;

    public void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void ContinueGame()
    {
        gameManager.LoadSettings();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void NewGame()
    {
        gameManager.ResetProgress();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OpenPlayGame()
    {
        playMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void ClosePlayGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        playMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void OpenOptions()
    {
        //Set control menu to true, main menu to false and current button to back
        optionsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void OpenCredits()
    {
        creditsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void CloseOptions()
    {
        //Set control menu to false, main menu to true and current button to play
        optionsMenu.SetActive(false);
        creditsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

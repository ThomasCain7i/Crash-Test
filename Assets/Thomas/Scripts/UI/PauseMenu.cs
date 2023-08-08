using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public bool isPaused = false;

    public GameObject pauseMenuUI, areYouSureUI, Main;

    [SerializeField]
    private LevelLoader levelLoader;

    private void Start()
    {
        levelLoader = FindObjectOfType<LevelLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void AreYouSure()
    {
        Main.SetActive(false);
        areYouSureUI.SetActive(true);
    }

    public void NotSure()
    {
        areYouSureUI.SetActive(false);
        Main.SetActive(true);
    }

    public void LoadMenu()
    {
        areYouSureUI.SetActive(false);
        Time.timeScale = 1f;
        levelLoader.LoadLevel(0);
    }
}
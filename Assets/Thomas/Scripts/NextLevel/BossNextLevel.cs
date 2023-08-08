using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNextLevel : MonoBehaviour
{
    [SerializeField]
    private GameObject bossMenuUI;

    [SerializeField]
    private bool isPaused;

    [SerializeField]
    private LevelLoader levelLoader;

    [SerializeField]
    private PlayerController playerController;

    private void Start()
    {
        levelLoader = FindObjectOfType<LevelLoader>();
    }

    public void Resume()
    {
        bossMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Paused()
    {
        bossMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void LoadBoss()
    {
        bossMenuUI.SetActive(false);
        Time.timeScale = 1f;
        levelLoader.LoadLevel(12);
    }
}

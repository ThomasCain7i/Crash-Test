using UnityEngine;
using UnityEngine.SceneManagement;

public class WinManager : MonoBehaviour
{
    [SerializeField]
    private bool sand;

    private PlayerController playerController;

    [SerializeField]
    private float collectableTimer1, collectableTimer2, collectableTimer3, collectableTimer4, collectableTimer5, SceneTimer;

    [SerializeField]
    private GameObject[] gameObjects;

    [SerializeField]
    private GameObject c1, c2, c3, c4, c5;

    [SerializeField]
    private int sandCollectables;
    private int fireCollectables;
    private int waterCollectables;
    private int snowCollectables;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>(); // Assign the PlayerController component to the playerController variable
        sandCollectables = playerController.SandBonusCount;
        fireCollectables = playerController.FireBonusCount;
        waterCollectables = playerController.WaterBonusCount;
        snowCollectables = playerController.SnowBonusCount;
    }

    // Update is called once per frame
    void Update()
    {
        collectableTimer1 -= Time.deltaTime;
        collectableTimer2 -= Time.deltaTime;
        collectableTimer3 -= Time.deltaTime;
        collectableTimer4 -= Time.deltaTime;
        collectableTimer5 -= Time.deltaTime;

        if (sand)
        {
            if (sandCollectables >= 0 && sandCollectables <= 5)
            {
                for (int i = 0; i < sandCollectables; i++)
                {
                    gameObjects[i].SetActive(true);
                }

                for (int i = sandCollectables; i < gameObjects.Length; i++)
                {
                    gameObjects[i].SetActive(false);
                }
            }
            else if (sandCollectables == 0)
            {
                for (int i = 0; i < gameObjects.Length; i++)
                {
                    gameObjects[i].SetActive(false);
                }
            }
        }

        if (collectableTimer1 <= 0)
        {
            c1.SetActive(true);
        }

        if (collectableTimer2 <= 0)
        {
            c2.SetActive(true);
        }

        if (collectableTimer3 <= 0)
        {
            c3.SetActive(true);
        }

        if (collectableTimer4 <= 0)
        {
            c4.SetActive(true);
        }

        if (collectableTimer5 <= 0)
        {
            c5.SetActive(true);
        }

        if (SceneTimer <= 0)
        {
            //hubWorldButton.SetActive(true);
        }
    }
}

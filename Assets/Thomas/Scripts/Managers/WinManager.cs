using UnityEngine;

public class WinManager : MonoBehaviour
{
    [SerializeField]
    private bool sand, fire, water, snow;

    private PlayerController playerController;

    [SerializeField]
    private float SceneTimer;

    [SerializeField]
    private GameObject[] gameObjects;

    [SerializeField]
    private GameObject hubWorld;

    [SerializeField]
    private int sandCollectables, fireCollectables, waterCollectables, snowCollectables;

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
        SceneTimer -= Time.deltaTime;

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

        if (fire)
        {
            if (fireCollectables >= 0 && fireCollectables <= 5)
            {
                for (int i = 0; i < fireCollectables; i++)
                {
                    gameObjects[i].SetActive(true);
                }

                for (int i = fireCollectables; i < gameObjects.Length; i++)
                {
                    gameObjects[i].SetActive(false);
                }
            }
            else if (fireCollectables == 0)
            {
                for (int i = 0; i < gameObjects.Length; i++)
                {
                    gameObjects[i].SetActive(false);
                }
            }
        }

        if (snow)
        {
            if (snowCollectables >= 0 && snowCollectables <= 5)
            {
                for (int i = 0; i < snowCollectables; i++)
                {
                    gameObjects[i].SetActive(true);
                }

                for (int i = snowCollectables; i < gameObjects.Length; i++)
                {
                    gameObjects[i].SetActive(false);
                }
            }
            else if (snowCollectables == 0)
            {
                for (int i = 0; i < gameObjects.Length; i++)
                {
                    gameObjects[i].SetActive(false);
                }
            }
        }

        if (water)
        {
            if (waterCollectables >= 0 && waterCollectables <= 5)
            {
                for (int i = 0; i < waterCollectables; i++)
                {
                    gameObjects[i].SetActive(true);
                }

                for (int i = waterCollectables; i < gameObjects.Length; i++)
                {
                    gameObjects[i].SetActive(false);
                }
            }
            else if (waterCollectables == 0)
            {
                for (int i = 0; i < gameObjects.Length; i++)
                {
                    gameObjects[i].SetActive(false);
                }
            }
        }

        if (SceneTimer <= 0)
        {
            hubWorld.SetActive(true);
        }
    }
}
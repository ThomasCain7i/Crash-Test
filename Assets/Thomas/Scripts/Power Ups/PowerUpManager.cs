using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerUpManager : MonoBehaviour
{
    [System.Serializable]
    public class PowerUpData
    {
        public GameObject powerUpObject;
        public float respawnTime;
        public bool isVisible;
        public bool isRespawning;
    }

    public List<PowerUpData> powerUps = new List<PowerUpData>();

    private void Start()
    {
        StartCoroutine(RespawnPowerUps());
    }

    private IEnumerator RespawnPowerUps()
    {
        while (true)
        {
            yield return null;

            for (int i = 0; i < powerUps.Count; i++)
            {
                PowerUpData powerUp = powerUps[i];

                if (!powerUp.isVisible && !powerUp.isRespawning)
                {
                    powerUp.isRespawning = true;
                    StartCoroutine(RespawnPowerUp(i, powerUp.respawnTime));
                }
            }
        }
    }

    private IEnumerator RespawnPowerUp(int index, float respawnTime)
    {
        PowerUpData powerUp = powerUps[index];
        yield return new WaitForSeconds(respawnTime);

        powerUp.powerUpObject.SetActive(true);
        powerUp.isVisible = true;
        powerUp.isRespawning = false;
    }

    public void DeactivatePowerUp(GameObject powerUpObject)
    {
        PowerUpData powerUp = powerUps.Find(p => p.powerUpObject == powerUpObject);

        if (powerUp != null && powerUp.isVisible)
        {
            powerUp.powerUpObject.SetActive(false);
            powerUp.isVisible = false;
        }
    }
}
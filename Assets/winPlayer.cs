using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winPlayer : MonoBehaviour
{
    private PlayerController playerController;

    public int sandCount, snowCount, fireCount, waterCount;


    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }
    // Start is called before the first frame update
    public void Update()
    {
        sandCount = playerController.SandBonusCount;
        snowCount = playerController.SnowBonusCount;
        fireCount = playerController.FireBonusCount;
        waterCount = playerController.WaterBonusCount;
    }
}

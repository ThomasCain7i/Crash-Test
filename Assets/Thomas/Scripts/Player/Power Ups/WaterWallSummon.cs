using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterWallSummon : MonoBehaviour
{
    public GameObject waterWall;
    public Transform wallTransform;
    [SerializeField]
    private float wallCooldown, wallCooldownNormal;
    [SerializeField]
    private AttackScript attackScript;

    private PlayerController playerController; 

    // Start is called before the first frame update
    void Start()
    {
        attackScript = GetComponent<AttackScript>();
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(wallCooldown > 0)
        {
            wallCooldown -= Time.deltaTime;
        }

        if (playerController.isGrounded)
        {
            if (attackScript.water == 1 && wallCooldown <= 0 && Input.GetKeyDown(KeyCode.T))
            {
                Instantiate(waterWall, wallTransform.position, wallTransform.rotation);
                wallCooldown = wallCooldownNormal;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialWall : MonoBehaviour
{
    private EnemyHealth enemyHealth;

    [SerializeField]
    private GameObject wall1, wall2;

    [SerializeField]
    private float health;

    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        health = enemyHealth.currentHealth;

        if (health <= 0)
        {
            Destroy(wall1);
            Destroy(wall2);
            Debug.Log("Destory Walls");
        }
    }
}

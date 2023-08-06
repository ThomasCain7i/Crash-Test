using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhase2 : State
{
    public GameObject projectilePrefab; // The prefab of the projectile to spawn
    public Transform[] spawnPoints;     // The positions where the projectiles will be spawned
    public float projectileSpeed = 5f;  // The speed at which the projectiles move
    public float spawnInterval = 3f;    // The interval between spawning projectiles

    [SerializeField]
    private Boss boss;
    [SerializeField]
    private BossPhase3 bossPhase3;

    private float timeSinceLastSpawn = 0f;
    private int currentSpawnPointIndex = 0;

    public override State RunCurrentState()
    {
        if (boss.phase4Reached)
        {
            return bossPhase3;
        }
        else
        {
            // Update timers
            timeSinceLastSpawn += Time.deltaTime;

            // Check if it's time to spawn a projectile
            if (timeSinceLastSpawn >= spawnInterval)
            {
                SpawnProjectiles();
                timeSinceLastSpawn = 0f;
            }

            return this; // Stay in the same phase for now
        }
    }

    private void SpawnProjectiles()
    {
        // Choose a random spawn point
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // Instantiate the projectile at the chosen spawn point
        GameObject newProjectile = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);
        Rigidbody projectileRb = newProjectile.GetComponent<Rigidbody>();

        if (projectileRb != null)
        {
            // Calculate the direction for the projectile to move
            Vector3 direction = spawnPoint.forward;

            // Set the velocity of the projectile
            projectileRb.velocity = direction * projectileSpeed;
        }
    }
}

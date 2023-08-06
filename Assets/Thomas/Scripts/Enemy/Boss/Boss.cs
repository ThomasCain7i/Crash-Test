using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject[] projectilePrefabs; // Array of projectile prefabs
    public Transform[] spawnPoints;        // The positions where the projectiles will be spawned
    public float projectileSpeed = 5f;    // The speed at which the projectiles move

    public float spawnInterval = 3f;       // The interval between spawning projectiles

    private float timeSinceLastSpawn = 3f;
    private int currentSpawnPointIndex = 0; // Index of the current spawn point to use

    private float phaseTimer = 0;

    private void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        phaseTimer += Time.deltaTime;

        // Check if it's time to spawn a projectile
        if (timeSinceLastSpawn >= spawnInterval)
        {
            SpawnProjectiles();
            timeSinceLastSpawn = 0f;
        }

        //Fire rate change
        if (phaseTimer > 5)
        {
            projectileSpeed = 10f;
        }
        if (phaseTimer > 10)
        {
            projectileSpeed = 15f;
        }
        if (phaseTimer > 15)
        {
            projectileSpeed = 20f;
        }
        if (phaseTimer > 20)
        {
            projectileSpeed = 25f;
        }
    }

    private void SpawnProjectiles()
    {
        int randomProjectileIndex = Random.Range(0, projectilePrefabs.Length);

        for (int i = 0; i < 4; i++)
        {
            Transform spawnPoint = spawnPoints[currentSpawnPointIndex];

            GameObject newProjectile = Instantiate(projectilePrefabs[randomProjectileIndex], spawnPoint.position, Quaternion.identity);
            Rigidbody projectileRb = newProjectile.GetComponent<Rigidbody>();

            if (projectileRb != null)
            {
                // Calculate the direction for the projectile to move
                Vector3 direction = spawnPoint.forward; // Use the spawn point's forward direction

                // Set the velocity of the projectile
                projectileRb.velocity = direction * projectileSpeed;
            }

            // Cycle through the spawn points (0, 1, 2, 3, 4, 0, ...)
            currentSpawnPointIndex = (currentSpawnPointIndex + 1) % spawnPoints.Length;
        }
    }
}
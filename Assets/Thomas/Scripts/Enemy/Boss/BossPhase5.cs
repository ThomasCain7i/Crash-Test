using UnityEngine;

public class BossPhase5 : State
{
    public GameObject[] projectilePrefabs; // Array of projectile prefabs
    public Transform[] spawnPoints;         // The positions where the projectiles will be spawned
    public float projectileSpeed = 5f;      // The speed at which the projectiles move
    public float spawnInterval = 3f;        // The interval between spawning projectiles

    private float timeSinceLastSpawn = 0f;
    private int currentSpawnPointIndex = 0;

    public override State RunCurrentState()
    {
        // Update timers
        timeSinceLastSpawn += Time.deltaTime;

        // Check if it's time to spawn projectiles
        if (timeSinceLastSpawn >= spawnInterval)
        {
            SpawnProjectiles();
            timeSinceLastSpawn = 0f;
        }

        return this; // Stay in the same phase for now
    }

    private void SpawnProjectiles()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            int randomProjectileIndex = Random.Range(0, projectilePrefabs.Length);

            GameObject newProjectile = Instantiate(projectilePrefabs[randomProjectileIndex], spawnPoint.position, Quaternion.identity);
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
}

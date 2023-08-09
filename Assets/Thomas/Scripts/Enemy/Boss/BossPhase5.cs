using UnityEngine;

public class BossPhase5 : State
{
    public GameObject[] projectilePrefabs; // Array of projectile prefabs
    public Transform[] spawnPoints;         // The positions where the projectiles will be spawned
    public float projectileSpeed = 5f;      // The speed at which the projectiles move
    public float shootInterval = 3f;        // The interval between shooting projectiles

    private float timeSinceLastShot = 0f;

    [SerializeField]
    private Boss boss;
    [SerializeField]
    private BossPhase6 bossPhase6;


    public override State RunCurrentState()
    {
        if (boss.phase6Reached)
        {
            return bossPhase6;
        }
        else
        {
            // Update timers
            timeSinceLastShot += Time.deltaTime;

            // Check if it's time to shoot projectiles
            if (timeSinceLastShot >= shootInterval)
            {
                ShootProjectiles();
                timeSinceLastShot = 0f;
            }

            return this; // Stay in the same phase for now
        }
    }

    private void ShootProjectiles()
    {
        // Shuffle the spawn points array using Fisher-Yates shuffle algorithm
        for (int i = spawnPoints.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            Transform temp = spawnPoints[i];
            spawnPoints[i] = spawnPoints[randomIndex];
            spawnPoints[randomIndex] = temp;
        }

        for (int i = 0; i < Mathf.Min(4, spawnPoints.Length); i++)
        {
            if (projectilePrefabs.Length > 0)
            {
                int randomProjectileIndex = Random.Range(0, projectilePrefabs.Length);
                GameObject selectedPrefab = projectilePrefabs[randomProjectileIndex];
                Transform spawnPoint = spawnPoints[i]; // Take the next shuffled spawn point
                SpawnPrefab(selectedPrefab, spawnPoint);
            }
        }
    }

    private void SpawnPrefab(GameObject prefab, Transform spawnPoint)
    {
        GameObject newProjectile = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
        Rigidbody projectileRb = newProjectile.GetComponent<Rigidbody>();

        // No need to set velocity here, as the projectile's own script should handle movement.
    }
}

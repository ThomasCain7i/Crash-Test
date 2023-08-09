using UnityEngine;

public class BossPhase6 : State
{
    public GameObject[] projectilePrefabs; // Array of projectile prefabs
    public Transform[] spawnPoints;         // The positions where the projectiles will be spawned
    public float projectileSpeed = 5f;      // The speed at which the projectiles move
    public float shootInterval = 3f;        // The interval between shooting projectiles

    private float timeSinceLastShot = 0f;

    public override State RunCurrentState()
    {
            return this; // Stay in the same phase for now
    }
}

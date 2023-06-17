using UnityEngine;

public class SnowPlatform : MonoBehaviour
{
    public float destroyTimer = 5f;

    private void Update()
    {
        Destroy(gameObject, destroyTimer);
    }
}
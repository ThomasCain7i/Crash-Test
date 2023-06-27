using UnityEngine;

public class SnowPlatform : MonoBehaviour
{
    public float destroyTimer = 5f;

    private void Update()
    {
        Destroy(gameObject, destroyTimer);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Touched Platform");
        }
    }
}
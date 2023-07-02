using UnityEngine;

public class BoulderTrigger : MonoBehaviour
{
    public GameObject floor; // Reference to the Floor GameObject

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            floor.SetActive(false);
        }
    }
}
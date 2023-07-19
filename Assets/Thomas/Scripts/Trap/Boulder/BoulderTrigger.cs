using UnityEngine;

public class BoulderTrigger : MonoBehaviour
{
    public GameObject floor; // Reference to the Floor GameObject
    public GameObject floor2; // Reference to the Floor GameObject
    public GameObject floor3; // Reference to the Floor GameObject

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            floor.SetActive(false);
            floor2.SetActive(false);
            floor3.SetActive(false);
        }
    }
}
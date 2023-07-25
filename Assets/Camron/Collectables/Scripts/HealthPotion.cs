using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    // Start is called before the first frame update
    public int rotateSpeed;
    [SerializeField]
    private PlayerControllerCam playerController;

    void Start()
    {
        playerController = FindObjectOfType<PlayerControllerCam>();
        rotateSpeed = 2;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0, rotateSpeed, 0, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && playerController.currentHealth != playerController.maxHealth)
        {
            if (playerController != null)
            {
                playerController.GainHealth();
                Debug.Log("Player picked up the health");
            }
            Destroy(gameObject);
        }
    }
}

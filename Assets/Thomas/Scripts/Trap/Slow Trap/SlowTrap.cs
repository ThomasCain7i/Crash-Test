using UnityEngine;

public class SlowTrap : MonoBehaviour
{
    public PlayerController playerController;
    [SerializeField]
    private int slowness = 3;
    public PlayerControllerCam pc;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        pc = FindObjectOfType<PlayerControllerCam>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Slowed");

            //collects powerup by setting the players bool to true
            playerController.moveSpeed -= slowness;
            playerController.isSlowed = true;
            pc.moveSpeed -= slowness;
            pc.isSlowed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("No longer slowed");

            //collects powerup by setting the players bool to true
            playerController.moveSpeed += slowness;
            playerController.isSlowed = false;
            pc.moveSpeed += slowness;
            pc.isSlowed = false;
        }
    }
}

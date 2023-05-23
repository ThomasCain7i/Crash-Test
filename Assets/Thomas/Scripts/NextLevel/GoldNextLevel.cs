using UnityEngine;
using UnityEngine.SceneManagement;

public class GoldNextLevel : MonoBehaviour
{
    public PlayerController playerController;
    public int bonesToCollect;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if(playerController.boneCount >= bonesToCollect)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}

// Thomas

using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [Header("References")]
    public PlayerController playerController;
    public int bonusToCollect = 5;
    public int bonus;

    [Header("Bools")]
    [SerializeField]
    private bool Fire;
    [SerializeField]
    private bool Water, Sand, Snow, Bonus;


    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        bonus = playerController.bonusCount;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Fire)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }

            if (Water)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
            }

            if (Snow)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
            }

            if (Sand)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 4);
            }

            if (Bonus && bonus > bonusToCollect)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 5);
            }
        }
    }
}

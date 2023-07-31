using UnityEngine;
using UnityEngine.SceneManagement;

public class CinematicSceneLoader : MonoBehaviour
{
    // Update is called once per frame
    void OnEnable()
    {
        SceneManager.LoadScene("Hub World", LoadSceneMode.Single);
    }
}
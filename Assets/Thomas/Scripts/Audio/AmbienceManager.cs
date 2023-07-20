using UnityEngine;

public class AmbienceManager : MonoBehaviour
{
    [Header("What Level")]
    public bool fire;
    public bool sand, water, snow;

    [Header("Audio Clips")]

    [Header("Ambience")]
    public AudioClip[] volcanoAmbience;
    public AudioClip[] paradiseAmbience;
    public AudioClip[] mountainAmbience;
    public AudioClip[] desertAmbience;

    [Header("Volume")]
    [SerializeField]
    private float desertAmbienceV;
    [SerializeField]
    private float snowAmbienceV, volcanoAmbienceV, paradiseAmbienceV;

    private AudioSource audioSource;
    private AudioSource audioSource2;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        PlayAmbience();
    }

    public void PlayAmbience()
    {
        if (volcanoAmbience.Length > 0 && fire == true)
        {
            int randomIndex = Random.Range(0, volcanoAmbience.Length);
            audioSource.clip = volcanoAmbience[randomIndex];
            audioSource.volume = volcanoAmbienceV;
            audioSource.loop = true;
            audioSource.Play();
            Debug.Log("Play theme");
        }

        if (mountainAmbience.Length > 0 && snow == true)
        {
            int randomIndex = Random.Range(0, mountainAmbience.Length);
            audioSource.clip = mountainAmbience[randomIndex];
            audioSource.volume = snowAmbienceV;
            audioSource.loop = true;
            audioSource.Play();
            Debug.Log("Play theme");
        }

        if (paradiseAmbience.Length > 0 && water == true)
        {
            int randomIndex = Random.Range(0, paradiseAmbience.Length);
            audioSource.clip = paradiseAmbience[randomIndex];
            audioSource.volume = paradiseAmbienceV;
            audioSource.loop = true;
            audioSource.Play();
            Debug.Log("Play theme");
        }

        if (desertAmbience.Length > 0 && sand == true)
        {
            int randomIndex = Random.Range(0, desertAmbience.Length);
            audioSource.clip = desertAmbience[randomIndex];
            audioSource.volume = desertAmbienceV;
            audioSource.loop = true;
            audioSource.Play();
            Debug.Log("Play theme");
        }
    }
}
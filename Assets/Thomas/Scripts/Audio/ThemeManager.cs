using UnityEngine;

public class ThemeManager : MonoBehaviour
{
    [Header("What Level")]
    public bool fire;
    public bool sand, water, snow;

    [Header("Audio Clips")]

    [Header("Theme Music")]
    public AudioClip[] volcanoTheme;
    public AudioClip[] desertTheme;
    public AudioClip[] paradiseTheme;
    public AudioClip[] mountainTheme;

    [Header("Volume")]
    [SerializeField]
    private float paradiseThemeV;
    [SerializeField]
    private float snowThemeV, desertThemeV, volcanoThemeV;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        PlayThemeMusic();
    }

    public void PlayThemeMusic()
    {
        if (volcanoTheme.Length > 0 && fire == true)
        {
            int randomIndex = Random.Range(0, volcanoTheme.Length);
            audioSource.clip = volcanoTheme[randomIndex];
            audioSource.volume = volcanoThemeV;
            audioSource.loop = true;
            audioSource.Play();
            Debug.Log("Play theme");
        }

        if (mountainTheme.Length > 0 && snow == true)
        {
            int randomIndex = Random.Range(0, mountainTheme.Length);
            audioSource.clip = mountainTheme[randomIndex];
            audioSource.volume = snowThemeV;
            audioSource.loop = true;
            audioSource.Play();
            Debug.Log("Play theme");
        }

        if (paradiseTheme.Length > 0 && water == true)
        {
            int randomIndex = Random.Range(0, paradiseTheme.Length);
            audioSource.clip = paradiseTheme[randomIndex];
            audioSource.volume = paradiseThemeV;
            audioSource.loop = true;
            audioSource.Play();
            Debug.Log("Play theme");
        }

        if (desertTheme.Length > 0 && sand == true)
        {
            int randomIndex = Random.Range(0, desertTheme.Length);
            audioSource.clip = desertTheme[randomIndex];
            audioSource.volume = desertThemeV;
            audioSource.loop = true;
            audioSource.Play();
            Debug.Log("Play theme");
        }
    }
}
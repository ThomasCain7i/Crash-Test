using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("What Level")]
    public bool fire;
    public bool sand, water, snow;

    [Header("Audio Clips")]
    [Header("Misc")]
    public AudioClip[] levelComplete;
    public AudioClip[] buttonClicked;

    [Header("Theme Music")]
    public AudioClip[] volcanoTheme;
    public AudioClip[] desertTheme;
    public AudioClip[] paradiseTheme;
    public AudioClip[] mountainTheme;

    [Header("Ambience")]
    public AudioClip[] volcanoAmbience;
    public AudioClip[] paradiseAmbience;
    public AudioClip[] mountainAmbience;
    public AudioClip[] desertAmbience;

    [Header("Enemies")]
    public AudioClip[] enemyShot;
    public AudioClip[] enemyMelee;
    public AudioClip[] enemyDamaged;
    public AudioClip[] enemyDeath;

    [Header("Volume")]
    [SerializeField]
    private float levelCompleteV;
    [SerializeField]
    private float buttonClickedV, bouncePadV, ThemeMusicV, AmbienceV, enemyShotV, enemyMeleeV, enemyDamagedV, enemyDeathV;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        PlayAmbience();
        PlayThemeMusic();
    }

    public void PlayLevelComplete()
    {
        if (levelComplete.Length > 0)
        {
            int randomIndex = Random.Range(0, levelComplete.Length);
            audioSource.PlayOneShot(levelComplete[randomIndex], levelCompleteV);
        }
    }

    public void PlayButtonClicked()
    {
        if (buttonClicked.Length > 0)
        {
            int randomIndex = Random.Range(0, buttonClicked.Length);
            audioSource.PlayOneShot(buttonClicked[randomIndex], buttonClickedV);
        }
    }

    public void PlayAmbience()
    {
        if (volcanoAmbience.Length > 0 && fire == true)
        {
            int randomIndex = Random.Range(0, volcanoAmbience.Length);
            audioSource.clip = volcanoAmbience[randomIndex];
            audioSource.volume = AmbienceV;
            audioSource.loop = true;
            audioSource.Play();
            Debug.Log("Play theme");
        }

        if (mountainAmbience.Length > 0 && snow == true)
        {
            int randomIndex = Random.Range(0, mountainAmbience.Length);
            audioSource.clip = mountainAmbience[randomIndex];
            audioSource.volume = AmbienceV;
            audioSource.loop = true;
            audioSource.Play();
            Debug.Log("Play theme");
        }

        if (paradiseAmbience.Length > 0 && water == true)
        {
            int randomIndex = Random.Range(0, paradiseAmbience.Length);
            audioSource.clip = paradiseAmbience[randomIndex];
            audioSource.volume = AmbienceV;
            audioSource.loop = true;
            audioSource.Play();
            Debug.Log("Play theme");
        }

        if (desertAmbience.Length > 0 && sand == true)
        {
            int randomIndex = Random.Range(0, desertAmbience.Length);
            audioSource.clip = desertAmbience[randomIndex];
            audioSource.volume = AmbienceV;
            audioSource.loop = true;
            audioSource.Play();
            Debug.Log("Play theme");
        }
    }

    public void PlayThemeMusic()
    {
        if (volcanoTheme.Length > 0 && fire == true)
        {
            int randomIndex = Random.Range(0, volcanoTheme.Length);
            audioSource.clip = volcanoTheme[randomIndex];
            audioSource.volume = ThemeMusicV;
            audioSource.loop = true;
            audioSource.Play();
            Debug.Log("Play theme");
        }

        if (mountainTheme.Length > 0 && snow == true)
        {
            int randomIndex = Random.Range(0, mountainTheme.Length);
            audioSource.clip = mountainTheme[randomIndex];
            audioSource.volume = ThemeMusicV;
            audioSource.loop = true;
            audioSource.Play();
            Debug.Log("Play theme");
        }

        if (paradiseTheme.Length > 0 && water == true)
        {
            int randomIndex = Random.Range(0, paradiseTheme.Length);
            audioSource.clip = paradiseTheme[randomIndex];
            audioSource.volume = ThemeMusicV;
            audioSource.loop = true;
            audioSource.Play();
            Debug.Log("Play theme");
        }

        if (desertTheme.Length > 0 && sand == true)
        {
            int randomIndex = Random.Range(0, desertTheme.Length);
            audioSource.clip = desertTheme[randomIndex];
            audioSource.volume = ThemeMusicV;
            audioSource.loop = true;
            audioSource.Play();
            Debug.Log("Play theme");
        }
    }

    public void PlayEnemyMelee()
    {
        if (enemyMelee.Length > 0)
        {
            int randomIndex = Random.Range(0, enemyMelee.Length);
            audioSource.PlayOneShot(enemyMelee[randomIndex], enemyMeleeV);
        }
    }

    public void PlayEnemyShot()
    {
        if (enemyShot.Length > 0)
        {
            int randomIndex = Random.Range(0, enemyShot.Length);
            audioSource.PlayOneShot(enemyShot[randomIndex], enemyShotV);
        }
    }

    public void PlayEnemyDeath()
    {
        if (enemyDeath.Length > 0)
        {
            int randomIndex = Random.Range(0, enemyDeath.Length);
            audioSource.PlayOneShot(enemyDeath[randomIndex], enemyDeathV);
        }
    }
}
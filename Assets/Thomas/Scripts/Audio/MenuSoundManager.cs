using UnityEngine;

public class MenuSoundManager : MonoBehaviour
{
    [Header("What Level")]
    public bool hub = true;

    [Header("Audio Clips")]
    [Header("Misc")]
    public AudioClip[] levelComplete;
    public AudioClip[] buttonClicked;

    [Header("Enemies")]
    public AudioClip[] enemyShot;
    public AudioClip[] enemyMelee;
    public AudioClip[] enemyDamaged;
    public AudioClip[] enemyDeath;

    [Header("Volume")]
    [SerializeField]
    private float levelCompleteV;
    [SerializeField]
    private float buttonClickedV, bouncePadV, enemyShotV, enemyMeleeV, enemyDamagedV, enemyDeathV;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
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
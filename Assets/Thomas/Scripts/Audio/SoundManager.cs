using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [Header("Audio Clips")]
    [Header("Misc")]
    public AudioClip[] levelComplete;
    [Header("Power Ups")]
    public AudioClip[] healing;
    [Header("Movement")]
    public AudioClip[] jump;
    public AudioClip[] walk;
    [Header("Attacks")]
    public AudioClip[] smashAttack;
    public AudioClip[] punchAttack;
    public AudioClip[] rangedAttack;
    [Header("S")]
    public AudioClip[] liftBoxSound;

    [Header("Volume")]
    [SerializeField]
    [Range(0.0f, 1f)]
    private float themeSlider;

    [SerializeField]
    private float fullVolume = 1f, halfVolume = .5f, lowVolume = 0.1f;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    /*
    public void PlayLevelCompleteSound()
    {
        if (levelComplete.Length > 0)
        {
            int randomIndex = Random.Range(0, levelComplete.Length);
            audioSource.PlayOneShot(levelComplete[randomIndex], fullVolume);
        }
    }

    public void PlayHealingSound()
    {
        if (healing.Length > 0)
        {
            int randomIndex = Random.Range(0, healing.Length);
            audioSource.PlayOneShot(healing[randomIndex], fullVolume);
        }
    }

    public void PlayJumpSound()
    {
        if (jump.Length > 0)
        {
            int randomIndex = Random.Range(0, jump.Length);
            audioSource.PlayOneShot(jump[randomIndex], fullVolume);
        }
    }

    public void PlayEnemyDeathSounds()
    {
        if (enemyDeathSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, enemyDeathSounds.Length);
            audioSource.PlayOneShot(enemyDeathSounds[randomIndex], fullVolume);
        }
    }

    public void PlayGirlAttackSounds()
    {
        if (girlAttackSound.Length > 0)
        {
            int randomIndex = Random.Range(0, girlAttackSound.Length);
            audioSource.PlayOneShot(girlAttackSound[randomIndex], fullVolume);
        }
    }

    public void PlayHitByShuriken()
    {
        if (hitByShurikenSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, hitByShurikenSounds.Length);
            audioSource.PlayOneShot(hitByShurikenSounds[randomIndex], fullVolume);
        }
    }

    public void PlayDropBoxSounds()
    {
        if (dropBoxSound.Length > 0)
        {
            int randomIndex = Random.Range(0, dropBoxSound.Length);
            audioSource.PlayOneShot(dropBoxSound[randomIndex], fullVolume);
        }
    }

    public void PlayLiftBoxSounds()
    {
        if (liftBoxSound.Length > 0)
        {
            int randomIndex = Random.Range(0, liftBoxSound.Length);
            audioSource.PlayOneShot(liftBoxSound[randomIndex], fullVolume);
        }
    }
    */
}
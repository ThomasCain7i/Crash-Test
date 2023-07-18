using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("What Level")]
    public bool fire;
    public bool sand, water, snow;

    [Header("Audio Clips")]
    [Header("Misc")]
    public AudioClip[] levelComplete;
    public AudioClip[] butonClicked;
    public AudioClip[] bouncePad;

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

    [Header("Power Ups")]
    public AudioClip[] healing;
    public AudioClip[] pickUp;
    public AudioClip[] collectable;

    [Header("Player")]
    public AudioClip[] jump;
    public AudioClip[] walk;
    public AudioClip[] swim;
    public AudioClip[] damaged;
    public AudioClip[] death;
    public AudioClip[] respawn;

    [Header("Attacks")]
    public AudioClip[] smashAttack;
    public AudioClip[] punchAttack;
    public AudioClip[] rangedAttack;

    [Header("Traps")]
    public AudioClip[] spikeTrap;
    public AudioClip[] arrowTrap;

    [Header("Enemies")]
    public AudioClip[] enemyShot;
    public AudioClip[] enemyMelee;
    public AudioClip[] enemyDeath;

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

        if (sand == true)
        {
            PlayAmbience();
        }
    }

    public void PlayLevelComplete()
    {
        if (levelComplete.Length > 0)
        {
            int randomIndex = Random.Range(0, levelComplete.Length);
            audioSource.PlayOneShot(levelComplete[randomIndex], fullVolume);
        }
    }

    public void PlayButtonClicked()
    {
        if (levelComplete.Length > 0)
        {
            int randomIndex = Random.Range(0, butonClicked.Length);
            audioSource.PlayOneShot(butonClicked[randomIndex], fullVolume);
        }
    }

    public void PlayBouncePad()
    {
        if (bouncePad.Length > 0)
        {
            int randomIndex = Random.Range(0, bouncePad.Length);
            audioSource.PlayOneShot(bouncePad[randomIndex], fullVolume);
        }
    }

    public void PlayAmbience()
    {
        if (volcanoAmbience.Length > 0 && fire == true)
        {
            int randomIndex = Random.Range(0, volcanoAmbience.Length);
            audioSource.clip = volcanoAmbience[randomIndex];
            audioSource.volume = fullVolume;
            audioSource.loop = true;
            audioSource.Play();
            Debug.Log("Play theme");
        }

        if (mountainAmbience.Length > 0 && snow == true)
        {
            int randomIndex = Random.Range(0, mountainAmbience.Length);
            audioSource.clip = mountainAmbience[randomIndex];
            audioSource.volume = fullVolume;
            audioSource.loop = true;
            audioSource.Play();
            Debug.Log("Play theme");
        }

        if (paradiseAmbience.Length > 0 && water == true)
        {
            int randomIndex = Random.Range(0, paradiseAmbience.Length);
            audioSource.clip = paradiseAmbience[randomIndex];
            audioSource.volume = fullVolume;
            audioSource.loop = true;
            audioSource.Play();
            Debug.Log("Play theme");
        }

        if (desertAmbience.Length > 0 && sand == true)
        {
            int randomIndex = Random.Range(0, desertAmbience.Length);
            audioSource.clip = desertAmbience[randomIndex];
            audioSource.volume = fullVolume;
            audioSource.loop = true;
            audioSource.Play();
            Debug.Log("Play theme");
        }
    }

    public void PlayHealing()
    {
        if (healing.Length > 0)
        {
            int randomIndex = Random.Range(0, healing.Length);
            audioSource.PlayOneShot(healing[randomIndex], fullVolume);
        }
    }

    public void PlayPickUp()
    {
        if (healing.Length > 0)
        {
            int randomIndex = Random.Range(0, pickUp.Length);
            audioSource.PlayOneShot(pickUp[randomIndex], fullVolume);
        }
    }

    public void PlayCollectable()
    {
        if (healing.Length > 0)
        {
            int randomIndex = Random.Range(0, collectable.Length);
            audioSource.PlayOneShot(collectable[randomIndex], fullVolume);
        }
    }

    public void PlayJump()
    {
        if (jump.Length > 0)
        {
            int randomIndex = Random.Range(0, jump.Length);
            audioSource.PlayOneShot(jump[randomIndex], fullVolume);
        }
    }

    public void PlayWalk()
    {
        if (walk.Length > 0)
        {
            int randomIndex = Random.Range(0, walk.Length);
            audioSource.PlayOneShot(walk[randomIndex], fullVolume);
        }
    }

    public void PlaySwim()
    {
        if (swim.Length > 0)
        {
            int randomIndex = Random.Range(0, swim.Length);
            audioSource.PlayOneShot(swim[randomIndex], fullVolume);
        }
    }

    public void PlayDamaged()
    {
        if (damaged.Length > 0)
        {
            int randomIndex = Random.Range(0, damaged.Length);
            audioSource.PlayOneShot(damaged[randomIndex], fullVolume);
        }
    }

    public void PlayDeath()
    {
        if (death.Length > 0)
        {
            int randomIndex = Random.Range(0, death.Length);
            audioSource.PlayOneShot(death[randomIndex], fullVolume);
        }
    }

    public void PlayRespawn()
    {
        if (respawn.Length > 0)
        {
            int randomIndex = Random.Range(0, respawn.Length);
            audioSource.PlayOneShot(respawn[randomIndex], fullVolume);
        }
    }

    public void PlaySmash()
    {
        if (smashAttack.Length > 0)
        {
            int randomIndex = Random.Range(0, smashAttack.Length);
            audioSource.PlayOneShot(smashAttack[randomIndex], fullVolume);
        }
    }

    public void PlayPunch()
    {
        if (punchAttack.Length > 0)
        {
            int randomIndex = Random.Range(0, punchAttack.Length);
            audioSource.PlayOneShot(punchAttack[randomIndex], fullVolume);
        }
    }

    public void PlayRanged()
    {
        if (rangedAttack.Length > 0)
        {
            int randomIndex = Random.Range(0, rangedAttack.Length);
            audioSource.PlayOneShot(rangedAttack[randomIndex], fullVolume);
        }
    }

    public void PlaySpike()
    {
        if (spikeTrap.Length > 0)
        {
            int randomIndex = Random.Range(0, spikeTrap.Length);
            audioSource.PlayOneShot(spikeTrap[randomIndex], fullVolume);
        }
    }

    public void PlayArrow()
    {
        if (arrowTrap.Length > 0)
        {
            int randomIndex = Random.Range(0, arrowTrap.Length);
            audioSource.PlayOneShot(arrowTrap[randomIndex], fullVolume);
        }
    }

    public void PlayEnemyMelee()
    {
        if (enemyMelee.Length > 0)
        {
            int randomIndex = Random.Range(0, enemyMelee.Length);
            audioSource.PlayOneShot(enemyMelee[randomIndex], fullVolume);
        }
    }

    public void PlayEnemyShot()
    {
        if (enemyShot.Length > 0)
        {
            int randomIndex = Random.Range(0, enemyShot.Length);
            audioSource.PlayOneShot(enemyShot[randomIndex], fullVolume);
        }
    }

    public void PlayEnemyDeath()
    {
        if (enemyDeath.Length > 0)
        {
            int randomIndex = Random.Range(0, enemyDeath.Length);
            audioSource.PlayOneShot(enemyDeath[randomIndex], fullVolume);
        }
    }
}
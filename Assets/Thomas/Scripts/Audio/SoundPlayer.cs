using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [Header("What Level")]
    public bool fire;
    public bool sand, water, snow;

    [Header("Misc")]
    public AudioClip[] bouncePad;

    [Header("Movement")]
    public AudioClip[] jump;
    public AudioClip[] land;
    public AudioClip[] swim;
    public AudioClip[] splash;

    [Header("Health")]
    public AudioClip[] damaged;
    public AudioClip[] death;
    public AudioClip[] respawn;

    [Header("Power Ups")]
    public AudioClip[] healing;
    public AudioClip[] pickUp;
    public AudioClip[] collectable;
    public AudioClip[] elemental;

    [Header("Attacks")]
    public AudioClip[] smashAttack;
    public AudioClip[] punchAttack;
    public AudioClip[] rangedAttack;

    [Header("Volume")]
    [SerializeField]
    private float bouncePadV;
    [SerializeField]
    private float healingV, pickUpV, collectableV, elementalV, jumpV, walkV, swimV, damagedV, deathV, respawnV, smashAttakV, punchAttackV, rangedAttackV;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayBouncePad()
    {
        if (bouncePad.Length > 0)
        {
            int randomIndex = Random.Range(0, bouncePad.Length);
            audioSource.PlayOneShot(bouncePad[randomIndex], bouncePadV);
        }
    }

    public void PlayHealing()
    {
        if (healing.Length > 0)
        {
            int randomIndex = Random.Range(0, healing.Length);
            audioSource.PlayOneShot(healing[randomIndex], healingV);
        }
    }

    public void PlayPickUp()
    {
        if (healing.Length > 0)
        {
            int randomIndex = Random.Range(0, pickUp.Length);
            audioSource.PlayOneShot(pickUp[randomIndex], pickUpV);
        }
    }

    public void PlayCollectable()
    {
        if (healing.Length > 0)
        {
            int randomIndex = Random.Range(0, collectable.Length);
            audioSource.PlayOneShot(collectable[randomIndex], collectableV);
        }
    }

    public void PlayJump()
    {
        if (jump.Length > 0)
        {
            int randomIndex = Random.Range(0, jump.Length);
            audioSource.PlayOneShot(jump[randomIndex], jumpV);
        }
    }


    public void PlaySwim()
    {
        if (swim.Length > 0)
        {
            int randomIndex = Random.Range(0, swim.Length);
            audioSource.PlayOneShot(swim[randomIndex], swimV);
        }
    }

    public void PlayDamaged()
    {
        if (damaged.Length > 0)
        {
            int randomIndex = Random.Range(0, damaged.Length);
            audioSource.PlayOneShot(damaged[randomIndex], damagedV);
        }
    }

    public void PlayDeath()
    {
        if (death.Length > 0)
        {
            int randomIndex = Random.Range(0, death.Length);
            audioSource.PlayOneShot(death[randomIndex], deathV);
        }
    }

    public void PlayRespawn()
    {
        if (respawn.Length > 0)
        {
            int randomIndex = Random.Range(0, respawn.Length);
            audioSource.PlayOneShot(respawn[randomIndex], respawnV);
        }
    }

    public void PlaySmash()
    {
        if (smashAttack.Length > 0)
        {
            int randomIndex = Random.Range(0, smashAttack.Length);
            audioSource.PlayOneShot(smashAttack[randomIndex], smashAttakV);
        }
    }

    public void PlayPunch()
    {
        if (punchAttack.Length > 0)
        {
            int randomIndex = Random.Range(0, punchAttack.Length);
            audioSource.PlayOneShot(punchAttack[randomIndex], punchAttackV);
        }
    }

    public void PlayRanged()
    {
        if (rangedAttack.Length > 0)
        {
            int randomIndex = Random.Range(0, rangedAttack.Length);
            audioSource.PlayOneShot(rangedAttack[randomIndex], rangedAttackV);
        }
    }
}

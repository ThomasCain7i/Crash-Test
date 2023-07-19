using UnityEngine;

public class SoundFootsteps : MonoBehaviour
{
    [Header("What Level")]
    public bool fire;
    public bool sand, water, snow;

    [Header("Footsteps")]
    public AudioClip[] walkSnow;
    public AudioClip[] walkSand;
    public AudioClip[] walkStone;

    [Header("Volume")]
    [SerializeField]
    private float walkV;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayWalk()
    {
        if (walkSand.Length > 0 && sand == true)
        {
            int randomIndex = Random.Range(0, walkSand.Length);
            audioSource.pitch = Random.Range(0.8f, 1.2f);
            audioSource.PlayOneShot(walkSand[randomIndex], walkV);
        }

        if (walkSnow.Length > 0 && snow == true)
        {
            int randomIndex = Random.Range(0, walkSnow.Length);
            audioSource.pitch = Random.Range(0.8f, 1.2f);
            audioSource.PlayOneShot(walkSnow[randomIndex], walkV);
        }

        if (walkStone.Length > 0 && fire == true || water == true)
        {
            int randomIndex = Random.Range(0, walkStone.Length);
            audioSource.pitch = Random.Range(0.8f, 1.2f);
            audioSource.PlayOneShot(walkStone[randomIndex], walkV);
        }
    }
}

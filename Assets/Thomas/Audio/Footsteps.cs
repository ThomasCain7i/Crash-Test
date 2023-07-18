using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip Snow;
    public AudioClip Sand;
    public AudioClip Stone;

    RaycastHit hit;
    public Transform rayStart;
    public float range = 3;
    public LayerMask layerMask;

    public void FootStepsSound()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {

                PlayFootstepSound(Snow);

            if (hit.collider.CompareTag("Stone"))
            {
                PlayFootstepSound(Stone);
            }
            if (hit.collider.CompareTag("Sand"))
            {
                PlayFootstepSound(Sand);
            }
        }
    }

    void PlayFootstepSound(AudioClip audio)
    {
        audioSource.pitch = Random.Range(0.8f, 1f);
        audioSource.PlayOneShot(audio);
    }

    private void Update()
    {
        Debug.DrawRay(rayStart.position, rayStart.transform.up * -1, Color.green);
    }
}

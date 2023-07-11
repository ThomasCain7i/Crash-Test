using UnityEngine;
using System.Collections;

public class DashScript : MonoBehaviour
{
    public PlayerController playerController;
    public AttackScript attackScript;
    public float dashTime, dashSpeed, dashCooldown, dashCooldownNormal;
    public GameObject dashParticle;
    public Transform dashParticlePos;
    private bool isDashing;

    // Start is called before the first frame update
    void Start()
    {
        attackScript = GetComponent<AttackScript>();
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDashing && Input.GetKeyDown(KeyCode.LeftShift) && dashCooldown <= 0 && attackScript.sand > 0)
        {
            StartCoroutine(Dash());
        }

        if (dashCooldown > 0)
        {
            dashCooldown -= Time.deltaTime;
        }
    }

    IEnumerator Dash()
    {
        isDashing = true;
        float startTime = Time.time;

        // Instantiate the dash particle system
        GameObject particleSystemObject = Instantiate(dashParticle, dashParticlePos.position, dashParticlePos.rotation);
        particleSystemObject.transform.SetParent(transform);

        ParticleSystem particleSystem = particleSystemObject.GetComponent<ParticleSystem>();

        while (Time.time < startTime + dashTime)
        {
            transform.Translate(Vector3.forward * dashSpeed * Time.deltaTime);
            Debug.Log("Dashing");

            yield return null;
        }

        isDashing = false;
        dashCooldown = dashCooldownNormal;

        // Stop emitting new particles
        particleSystem.Stop();

        // Wait for the remaining particles to finish
        yield return new WaitForSeconds(particleSystem.main.duration);

        // Destroy the dash particle system
        Destroy(particleSystemObject);
    }
}

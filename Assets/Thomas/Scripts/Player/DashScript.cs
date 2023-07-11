using UnityEngine;

public class DashScript : MonoBehaviour
{
    public Rigidbody rb;
    public float dashSpeed, dashTime;
    bool isDashing;

    public GameObject dashEffect;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            isDashing = true;
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
            Dashing();
    }

    private void Dashing()
    {
        rb.AddForce(transform.forward * dashSpeed, ForceMode.Impulse);
        isDashing = false;

        GameObject effect = Instantiate(dashEffect, Camera.main.transform.position, dashEffect.transform.rotation);
        effect.transform.parent = Camera.main.transform;
        effect.transform.LookAt(transform);
    }
}

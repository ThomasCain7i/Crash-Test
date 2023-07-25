using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTreatC : MonoBehaviour
{
    // Start is called before the first frame update
    public int rotateSpeed;
    public float damage;
    void Start()
    {
        rotateSpeed = 2;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0, rotateSpeed, 0, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("dog got treat");
        if (other.tag == "Player")
        {
            PlayerControllerCam playerController = other.GetComponent<PlayerControllerCam>();
            if (playerController != null)
            {
                playerController.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}

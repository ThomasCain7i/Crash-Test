using UnityEngine;

public class WaterWall : MonoBehaviour
{
    [SerializeField]
    private float Timer, speed = .1f;

    [SerializeField]
    private float damage;

    [SerializeField]
    private bool moving;

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, Timer);

        while (moving)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        //On collision with bullet do:
        switch (other.gameObject.tag)
        {
            case "Punch":
                Debug.Log("punched");
                moving = true;
                Timer += 3;
                break;
            // Collision with wall will destroy enemy bullets
            case "EnemyProjectile":
                Destroy(other.gameObject);
                break;
            // Collision with enemy deal damage
            case "Enemy":
                other.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
                break;
        }
    }
}
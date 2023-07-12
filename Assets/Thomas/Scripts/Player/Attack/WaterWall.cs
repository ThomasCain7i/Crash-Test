using UnityEngine;

public class WaterWall : MonoBehaviour
{
    [SerializeField]
    private float Timer, speed;

    [SerializeField]
    private float damage;

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, Timer);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        //On collision with bullet do:
        switch (other.gameObject.tag)
        {
            case "Punch":
                this.transform.Translate(Vector3.forward * speed * Time.deltaTime);
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
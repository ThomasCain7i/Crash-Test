using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedProjectileScript : MonoBehaviour
{
    public float speed;
    public float spawnTimer;

    public RangedObjectPool rangedObjectPool;
    public PlayerControllerRIO playerController;

    // Move the bullet forward
    public void RangedAttack()
    {
        // Moves position it travels based on player position

        
        if (playerController.isFacingLeft == true)
        {
            
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        else if (playerController.isFacingRight == true)
        {
            
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        else if (playerController.isFacingForwards == true)
        {
           transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else if (playerController.isFacingBackwards == true)
        {
           transform.Translate(Vector3.back * speed * Time.deltaTime);
        }
        


        // Set the projectile to active
        gameObject.SetActive(true);

            // Move the projectile forward
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        


    }
    // Start is called before the first frame update
    void Start()
    {
        speed = 8;
        rangedObjectPool = FindObjectOfType<RangedObjectPool>();
        playerController = FindObjectOfType<PlayerControllerRIO>();

       
    }

    // Update is called once per frame
    void Update()
    {
        RangedAttack();
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= 4)
        {
            gameObject.SetActive(false);
            spawnTimer = 0;
        }
        else if (spawnTimer <= 3.9f)
        {
            gameObject.SetActive(true);
        }

    }



    private void OnCollisionEnter2D(Collision2D collision)
    {



        if (collision.gameObject.tag == "Enemy")
        {

            gameObject.SetActive(false);


        }
        else if (collision.gameObject.tag == "Ground")
        {

            gameObject.SetActive(false);

        }
    }
}

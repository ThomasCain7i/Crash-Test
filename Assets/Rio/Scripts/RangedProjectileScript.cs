using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedProjectileScript : MonoBehaviour
{
    public float speed;
    public float spawnTimer;

    public RangedObjectPool rangedObjectPool;
    public PlayerController playerController;

    // Move the bullet forward
    public void RangedAttack()
    {
        // Moves position it travels based on player position

        /*
        if (playerController.moveHorizontal < 0)
        {
            transform.rotation = Quaternion.Euler(0f, 270f, 0f);
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        else if (playerController.moveHorizontal > 0)
        {
            transform.rotation = Quaternion.Euler(0f, 90f, 0f);
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        else if (moveVertical < 0)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else if (moveVertical > 0)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        */


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
        playerController = FindObjectOfType<PlayerController>();

       
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

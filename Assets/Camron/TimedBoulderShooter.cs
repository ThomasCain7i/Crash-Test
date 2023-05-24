using UnityEngine;

public class TimedBoulderShooter : MonoBehaviour
{
    //Arrows
    public Transform arrowSpawn;
    public GameObject arrowPrefab;
    public float arrowSpeed = 8f;

    //Cooldown
    public float coolDownTime;

    // Update is called once per frame
    void Update()
    {
        coolDownTime -= Time.deltaTime;

        if (coolDownTime <= 0)
        {
            var ability = Instantiate(arrowPrefab, arrowSpawn.position, arrowSpawn.rotation);
            ability.GetComponent<Rigidbody>().velocity = arrowSpawn.forward * arrowSpeed;

            //COOLDOWN FOR THE ABILITY 
            coolDownTime = 1f;
        }
    }
}
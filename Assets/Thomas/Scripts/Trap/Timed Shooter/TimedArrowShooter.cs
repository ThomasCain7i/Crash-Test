using UnityEngine;

public class TimedArrowShooter : MonoBehaviour
{
    //Arrows
    public Transform arrowSpawn;
    public GameObject arrowPrefab;
    public float arrowSpeed = 8f;

    //Cooldown
    public float coolDownTime;
    public float startCoolDownTime;
    public float coolDownTimeNormal;

    // Update is called once per frame
    void Update()
    {
        if (startCoolDownTime > 0)
        {
            startCoolDownTime -= Time.deltaTime;
        }
        else
        {
            coolDownTime -= Time.deltaTime;

            if (coolDownTime <= 0)
            {
                var ability = Instantiate(arrowPrefab, arrowSpawn.position, arrowSpawn.rotation);
                ability.GetComponent<Rigidbody>().velocity = arrowSpawn.forward * arrowSpeed;

                //COOLDOWN FOR THE ABILITY 
                coolDownTime = coolDownTimeNormal;
            }
        }

    }
}
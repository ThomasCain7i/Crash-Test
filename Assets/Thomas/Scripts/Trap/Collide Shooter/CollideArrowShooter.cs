using UnityEngine;

public class CollideArrowShooter : MonoBehaviour
{
    public Transform arrowSpawn;
    public GameObject arrowPrefab;
    public float arrowSpeed = 8f;

    // Update is called once per frame
    public void Shoot()
    {
        var ability = Instantiate(arrowPrefab, arrowSpawn.position, arrowSpawn.rotation);
        ability.GetComponent<Rigidbody>().velocity = arrowSpawn.forward * arrowSpeed;
    }
}

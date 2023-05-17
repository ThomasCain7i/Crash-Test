using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedObjectPool : MonoBehaviour
{ 
    // Holds reference to rangedProjectile GameObjects in the pool
    public List<GameObject> rangedProjectile;
    public GameObject rangedPrefab;
    // Number of projectiles to create in pool
    public int numProjectiles;



    private void Awake()
    {
        // Instantiate and disable each rangedProjectile GameObject and add it to the pool list
        rangedProjectile = new List<GameObject>();
        for (int i = 0; i < numProjectiles; i++)
        {
            // Instantiate each GameObject using tag
            GameObject projectile = Instantiate(rangedPrefab);

            // Set the GameObject as inactivve
            projectile.SetActive(false);
            // Add projectile to pool list
            rangedProjectile.Add(projectile);
        }
    }

    // Get projectile GameObject from pool
    public GameObject GetRangedProjectile()
    {
        foreach (GameObject obj in rangedProjectile)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }


        }

        GameObject newObj = Instantiate(Resources.Load("RangedProjectile"), Vector3.zero, Quaternion.identity) as GameObject;
        newObj.SetActive(false);
        rangedProjectile.Add(newObj);
        return newObj;
    }

    public void ReturnRangedProjectile(GameObject obj)
    {
        obj.SetActive(false);
    }

}

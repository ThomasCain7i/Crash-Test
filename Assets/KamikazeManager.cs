using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeManager : MonoBehaviour
{

    public SelfDestructEnemy kamikaze; 
    // Start is called before the first frame update
    void Start()
    {
        kamikaze.enabled = true; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

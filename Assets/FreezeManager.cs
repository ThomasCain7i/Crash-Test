using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeManager : MonoBehaviour
{
    public FreezeEnemy freeze; 

    // Start is called before the first frame update
    void Start()
    {
        freeze.enabled = true; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

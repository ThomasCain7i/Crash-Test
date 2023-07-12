using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCustomizer : MonoBehaviour
{
    [SerializeField]
    private AttackScript attackScript;

    [SerializeField]
    private Material material, material2, material3, material4, material5, material6, material7, material8, material9, material10;

    // Update is called once per frame
    void Update()
    {
        if (attackScript.snow == 1)
        {

        }

        if (attackScript.fire == 1)
        {

        }

        if (attackScript.water == 1)
        {

        }

        if (attackScript.sand == 1)
        {

        }
    }
}

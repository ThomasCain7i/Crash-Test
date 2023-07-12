using UnityEngine;

public class PlayerCustomizer : MonoBehaviour
{
    [Header("AttackScript")]
    [SerializeField]
    private AttackScript attackScript;

    [Header("Body Parts")]
    [SerializeField]
    private GameObject[] bodyParts;

    [Header("Materials")]
    [SerializeField]
    private Material[] materials;

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

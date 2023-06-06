using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformInfo
{
    public Vector3 position;
    public Quaternion rotation;
}

public class LSystem : MonoBehaviour
{
    private Stack<TransformInfo> transformStack;
    private Dictionary< /*Type:*/char,/*Value:*/string> rules;

    // Start is called before the first frame update
    void Start()
    {
        
    }
}

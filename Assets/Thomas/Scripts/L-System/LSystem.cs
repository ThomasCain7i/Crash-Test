using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;
using UnityEngine;

public class TransformInfo
{
    public Vector3 position;
    public Quaternion rotation;
}

public class LSystem : MonoBehaviour
{
    [SerializeField] private int iterations = 4; //no. iterations we want
    [SerializeField] private GameObject Branch;
    [SerializeField] private float length = 10f; // value for lengh of moving lines
    [SerializeField] private float angle = 30f;    // Angle value

    private const string axiom = "X"; // The OG rule.
    private string currentString = string.Empty;

    private Stack<TransformInfo> transformStack;
    private Dictionary< /*type:*/char,/*Value:*/string> rules;

    void Start()
    {
        transformStack = new Stack<TransformInfo>();

        rules = new Dictionary<char, string>
        {
            {'X', "[F-[[X]+X]+F[+FX]-X]"},
            {'F', "FF"}
        };

        Generate();
    }

    private void Generate()
    {
        currentString = axiom;

        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < iterations; i++)
        {
            foreach (char c in currentString)
            {
                // *Append() adds the value to the StringBuilder
                //Term of statement: if rules contains the key, then add value corresponding that key. 
                //Otherwise, add the value itself
                sb.Append(rules.ContainsKey(c) ? rules[c] : c.ToString());
            }

            currentString = sb.ToString();
            //reset the tring builder everytime:
            sb = new StringBuilder();
        }

        foreach (char c in currentString)
        {
            switch (c)
            {
                //F draws straign line
                case 'F':
                    //Start with storing initial position
                    Vector3 initialPosition = transform.position;
                    //Now we are going to move the treespawner
                    transform.Translate(Vector3.up * length);
                    //Using the branch we created earlier(prefab),
                    //Lets stantiate it
                    GameObject treeSegment = Instantiate(Branch);
                    //Set the line renderer Values
                    treeSegment.GetComponent<LineRenderer>().SetPosition(0, initialPosition);
                    treeSegment.GetComponent<LineRenderer>().SetPosition(1, transform.position);
                    break;

                //X generates more 'F's
                case 'X': //We don't want it to do anything so leave it...
                    break;

                //+ Clockwise rotation
                case '+':
                    transform.Rotate(Vector3.back * angle);
                    break;

                //- Anti-clockwise rotation
                case '-':
                    transform.Rotate(Vector3.forward * angle);
                    break;

                //[ save current transform info
                case '[':
                    transformStack.Push(new TransformInfo()
                    {
                        position = transform.position,
                        rotation = transform.rotation
                    });
                    break;

                //] return to the previously saved transform info 
                case ']':
                    //new verabiel for Transform Infor (TI)
                    TransformInfo ti = transformStack.Pop();
                    //Set our position to the position of TI
                    transform.position = ti.position;
                    //Set our rotation to the rotation of TI
                    transform.rotation = ti.rotation;
                    break;

                default:
                    throw new InvalidOperationException("Your L-System is Invalid, try again!");
            }
        }
    }
}
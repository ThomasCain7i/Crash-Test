using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialWall : MonoBehaviour
{
    [SerializeField]
    private GameObject wall1, wall2;

    // Update is called once per frame
    public void DeleteWalls()
    {
            Destroy(wall1);
            Destroy(wall2);
            Debug.Log("Destory Walls");
    }
}

using UnityEngine;

public class BossTriggers : MonoBehaviour
{
    private Boss boss;

    [SerializeField]
    private bool one,two,three,four,five;

    private void OnTriggerEnter(Collider other)
    {
        if(one)
        {
            if (other.CompareTag("Player") && !boss.phase1Reached)
            {
                boss.phase1Reached = true;
                boss.phase2Reached = false;
                boss.phase3Reached = false;
                boss.phase4Reached = false;
                boss.phase5Reached = false;
            }
        }

        if (two)
        {
            if (other.CompareTag("Player") && !boss.phase2Reached)
            {
                boss.phase1Reached = false;
                boss.phase2Reached = true;
                boss.phase3Reached = false;
                boss.phase4Reached = false;
                boss.phase5Reached = false;
            }
        }

        if (three)
        {
            if (other.CompareTag("Player") && !boss.phase3Reached)
            {
                boss.phase1Reached = false;
                boss.phase2Reached = false;
                boss.phase3Reached = true;
                boss.phase4Reached = false;
                boss.phase5Reached = false;
            }
        }

        if (other.CompareTag("Player") && !boss.phase4Reached)
        {
                boss.phase1Reached = false;
                boss.phase2Reached = false;
                boss.phase3Reached = false;
                boss.phase4Reached = true;
                boss.phase5Reached = false;
        }
        else if (other.CompareTag("Player") && !boss.phase5Reached)
        {
                boss.phase1Reached = false;
                boss.phase2Reached = false;
                boss.phase3Reached = false;
                boss.phase4Reached = false;
                boss.phase5Reached = true;
        }
    }
}

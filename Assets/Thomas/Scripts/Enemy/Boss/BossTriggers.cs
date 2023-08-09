using UnityEngine;

public class BossTriggers : MonoBehaviour
{
    private Boss boss;

    [SerializeField]
    private bool one,two,three,four,five, six;

    private void Start()
    {
        boss = FindObjectOfType<Boss>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(this.one == true)
        {
            if (other.CompareTag("Player") && !boss.phase1Reached)
            {
                Debug.Log("Phase 1 hit");
                boss.phase1Reached = true;
                boss.phase2Reached = false;
                boss.phase3Reached = false;
                boss.phase4Reached = false;
                boss.phase5Reached = false;
                boss.phase6Reached = false;
            }
        }

        if (this.two == true)
        {
            if (other.CompareTag("Player") && !boss.phase2Reached)
            {
                Debug.Log("Phase 2 hit");
                boss.phase1Reached = false;
                boss.phase2Reached = true;
                boss.phase3Reached = false;
                boss.phase4Reached = false;
                boss.phase5Reached = false;
                boss.phase6Reached = false;
            }
        }

        if (this.three)
        {
            if (other.CompareTag("Player") && !boss.phase3Reached)
            {
                Debug.Log("Phase 3 hit");
                boss.phase1Reached = false;
                boss.phase2Reached = false;
                boss.phase3Reached = true;
                boss.phase4Reached = false;
                boss.phase5Reached = false;
                boss.phase6Reached = false;
            }
        }

        if (this.four)
        {
        if (other.CompareTag("Player") && !boss.phase4Reached)
        {
            Debug.Log("Phase 4 hit");
                boss.phase1Reached = false;
                boss.phase2Reached = false;
                boss.phase3Reached = false;
                boss.phase4Reached = true;
                boss.phase5Reached = false;
                boss.phase6Reached = false;
        }
        }

       if (this.five)
        {
        if (other.CompareTag("Player") && !boss.phase5Reached)
        {
            Debug.Log("Phase 5 hit");
                boss.phase1Reached = false;
                boss.phase2Reached = false;
                boss.phase3Reached = false;
                boss.phase4Reached = false;
                boss.phase5Reached = true;
                boss.phase6Reached = false;
        }
        }

        if (this.six)
        {
            if (other.CompareTag("Player") && !boss.phase6Reached)
            {
                Debug.Log("Phase 5 hit");
                boss.phase1Reached = false;
                boss.phase2Reached = false;
                boss.phase3Reached = false;
                boss.phase4Reached = false;
                boss.phase5Reached = false;
                boss.phase6Reached = true;
            }
        }


    }
}

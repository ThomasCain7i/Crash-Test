using UnityEngine;

public class Boss : MonoBehaviour
{
    public bool phase1Reached = false;
    public bool phase2Reached = false;
    public bool phase3Reached = false;
    public bool phase4Reached = false;
    public bool phase5Reached = false;

    public float projectileSpeed;

    private State currentState;

    private void Update()
    {
        if (phase1Reached)
        {
            currentState = new BossPhase1();
        }

        if (phase2Reached)
        {
            currentState = new BossPhase2();
        }

        if (phase3Reached)
        {
            currentState = new BossPhase3();
        }

        if (phase4Reached)
        {
            currentState = new BossPhase4();
        }

        if (phase5Reached)
        {
            currentState = new BossPhase5();
        }

        // Execute current phase behavior
        if (currentState != null)
        {
            currentState = currentState.RunCurrentState();
        }
    }
}

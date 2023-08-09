using UnityEngine;

public class Boss : MonoBehaviour
{
    public bool phase1Reached = false;
    public bool phase2Reached = false;
    public bool phase3Reached = false;
    public bool phase4Reached = false;
    public bool phase5Reached = false;
    public bool phase6Reached = false;

    public float projectileSpeed;

    private State currentState;

    private void Update()
    {
        if (phase1Reached)
        {
            currentState = new BossPhase1();
            Vector3 newPosition = new Vector3(15f, 1f, 100f);
            transform.position = newPosition;
            projectileSpeed = 20;
        }

        if (phase2Reached)
        {
            currentState = new BossPhase2();
            Vector3 newPosition = new Vector3(15f, 1f, 200f);
            transform.position = newPosition;
            projectileSpeed = 25;
        }

        if (phase3Reached)
        {
            currentState = new BossPhase3();
            Vector3 newPosition = new Vector3(15f, 1f, 300f);
            transform.position = newPosition;
            projectileSpeed = 30;

        }

        if (phase4Reached)
        {
            currentState = new BossPhase4();
            Vector3 newPosition = new Vector3(15f, 1f, 400f);
            transform.position = newPosition;
            projectileSpeed = 35;
        }

        if (phase5Reached)
        {
            currentState = new BossPhase5();
            Vector3 newPosition = new Vector3(15f, 1f, 500f);
            transform.position = newPosition;
            projectileSpeed = 40;
        }

        if (phase6Reached)
        {
            //currentState = new BossPhase6();
            Vector3 newPosition = new Vector3(15f, 1f, 600);
            transform.position = newPosition;
            projectileSpeed = 40;
        }

        // Execute current phase behavior
        if (currentState != null)
        {
            currentState = currentState.RunCurrentState();
        }
    }
}

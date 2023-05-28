using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public State currentState;

    // Start is called before the first frame update
    void Update()
    {
        RunStateMachine();
    }

    // Update is called once per frame
    void RunStateMachine()
    {
        //What is the current state
        State nextState = currentState?.RunCurrentState();

        if (nextState != null)
        {
            //Swtich to the next state
            SwitchToTheNextState(nextState);
        }
    }

    private void SwitchToTheNextState(State nextState)
    {
        currentState = nextState;
    }
}

using UnityEngine;

public class PlayerStateMachine
{
    private BaseState curState;
    public PlayerStateMachine(BaseState initState)
    {
        curState = initState;
        ChangeState(curState);
    }

    public void ChangeState(BaseState nextState)
    {
        if (curState == nextState)
            return;

        if(curState != null)
        {
            curState.OnStateExit();
        }

        curState = nextState;
        curState.OnStateEnter();
    }

    public void UpdateState()
    {
        if (curState != null)
            curState.OnStateUpdate();
    }
}

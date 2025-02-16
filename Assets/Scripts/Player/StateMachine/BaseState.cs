using UnityEngine;

public abstract class BaseState
{
    protected Player player;
    protected PlayerStateMachine stateMachine;

    protected BaseState(Player _player, PlayerStateMachine stateMachine)
    {
        this.player = _player;
        this.stateMachine = stateMachine;
    }

    public abstract void OnStateEnter();
    public abstract void OnStateUpdate();
    public abstract void OnStateExit();
}

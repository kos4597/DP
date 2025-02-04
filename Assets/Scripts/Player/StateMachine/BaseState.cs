using UnityEngine;

public abstract class BaseState
{
    public enum StateType
    {
        Idle,
        Move,
        Attack,
    }

    protected Player player;

    protected BaseState(Player _player)
    {
        player = _player;
    }

    public abstract void OnStateEnter();
    public abstract void OnStateUpdate();
    public abstract void OnStateExit();
}

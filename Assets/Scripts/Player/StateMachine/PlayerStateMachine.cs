using System.Collections.Generic;
using UnityEngine;

public enum StateType
{
    None,
    Idle,
    Move,
    Attack,

    Max,
}

public class PlayerStateMachine
{

    private Dictionary<StateType, BaseState> stateDic = null;

    public PlayerStateMachine(Player player)
    {
        stateDic = new Dictionary<StateType, BaseState>((int)StateType.Max);

        stateDic.Add(StateType.Idle, new IdleState(player, this));
        stateDic.Add(StateType.Move, new MoveState(player, this));
        stateDic.Add(StateType.Attack, new AttackState(player, this));   
    }

    public StateType CurrentStateType { get; private set; }

    public void ChangeState(StateType stateType)
    {
        if (CurrentStateType == stateType)
            return;

        Debug.Log($"ChangeState : {stateType}");

        if(CurrentStateType != StateType.None)
        {
            stateDic[CurrentStateType].OnStateExit();
        }

        CurrentStateType = stateType;
        stateDic[CurrentStateType].OnStateEnter();
    }

    public void UpdateState()
    {
        stateDic[CurrentStateType].OnStateUpdate();
    }
}

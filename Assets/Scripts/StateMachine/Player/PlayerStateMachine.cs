using System.Collections.Generic;
using UnityEngine;

public enum PlayerStateType
{
    None,

    Idle,
    Move,
    Attack,
    Skill,

    Max,
}

public class PlayerStateMachine : IStateMachine<PlayerStateType>
{
    private Dictionary<PlayerStateType, BaseState> stateDic = null;
    public PlayerStateType CurrentStateType { get; private set; }

    public PlayerStateMachine(Player player)
    {
        stateDic = new Dictionary<PlayerStateType, BaseState>((int)PlayerStateType.Max)
        {
            { PlayerStateType.Idle, new IdleState(player, this) },
            { PlayerStateType.Move, new MoveState(player, this) },
            { PlayerStateType.Attack, new AttackState(player, this) },
            { PlayerStateType.Skill, new SkillState(player,this) }
        };
    }

    public void ChangeState(PlayerStateType stateType)
    {
        if (CurrentStateType == stateType)
            return;

        Debug.Log($"ChangeState : {stateType}");

        if(CurrentStateType != PlayerStateType.None)
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

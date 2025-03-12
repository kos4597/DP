using System.Collections.Generic;
using UnityEngine;
public enum MonsterStateType
{
    None,

    Idle,
    Patrol,
    Tracking,
    Attack,
    RunAway,
    Dead,

    Max,
}

public class MonsterStateMachine : IStateMachine<MonsterStateType>
{
    private Dictionary<MonsterStateType, BaseState> stateDic = null;
    public MonsterStateType CurrentStateType { get; private set; }

    public MonsterStateMachine(Monster monster)
    {
        stateDic = new Dictionary<MonsterStateType, BaseState>((int)MonsterStateType.Max)
        {
            { MonsterStateType.Idle, new MonsterIdleState(monster, this) },
            { MonsterStateType.Patrol, new PatrolState(monster, this) },
            { MonsterStateType.Tracking, new TrackingState(monster, this) },
            { MonsterStateType.Attack, new MonsterAttackState(monster, this) },
            { MonsterStateType.RunAway, new RunAwayState(monster, this) },
            { MonsterStateType.Dead, new DeadState(monster, this) }
        };
    }

    public void ChangeState(MonsterStateType stateType)
    {
        if (CurrentStateType == stateType)
            return;

        Debug.LogWarning($"ChangeState : {stateType}");

        if (CurrentStateType != MonsterStateType.None)
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

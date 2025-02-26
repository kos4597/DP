using UnityEngine;

public abstract class BaseState
{
    protected Player player;
    protected IStateMachine<PlayerStateType> playerStateMachine;

    protected Monster monster;
    protected IStateMachine<MonsterStateType> monsterStateMachine;

    protected BaseState(Player _player, IStateMachine<PlayerStateType> stateMachine)
    {
        this.player = _player;
        this.playerStateMachine = stateMachine;
    }

    protected BaseState(Monster _Monster, IStateMachine<MonsterStateType> stateMachine)
    {
        this.monster = _Monster;
        this.monsterStateMachine = stateMachine;
    }

    public abstract void OnStateEnter();
    public abstract void OnStateUpdate();
    public abstract void OnStateExit();
}

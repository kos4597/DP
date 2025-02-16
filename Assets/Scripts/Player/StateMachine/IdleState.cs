using UnityEngine;

public class IdleState : BaseState
{
    public IdleState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

    public override void OnStateEnter()
    {
        player.SetAnimaion(StringDefine.IDLE_ANI_HASH, true);
    }

    public override void OnStateUpdate()
    {
        if (player.CheckAttack())
        {
            stateMachine.ChangeState(StateType.Attack);
        }
        else if (player.CheckMoveInput())
        {
            stateMachine.ChangeState(StateType.Move);
        }
    }

    public override void OnStateExit()
    {
        player.SetAnimaion(StringDefine.IDLE_ANI_HASH, false);
    }
}

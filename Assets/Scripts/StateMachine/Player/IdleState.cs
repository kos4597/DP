using UnityEngine;

public class IdleState : BaseState
{
    public IdleState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

    public override void OnStateEnter()
    {
        player.GetAnimator().SetAnimaion(StringDefine.IDLE_ANI_HASH, true);
    }

    public override void OnStateUpdate()
    {
        if (player.CheckAttack())
        {
            playerStateMachine.ChangeState(PlayerStateType.Attack);
        }
        else if (player.CheckMoveInput())
        {
            playerStateMachine.ChangeState(PlayerStateType.Move);
        }
    }

    public override void OnStateExit()
    {
        player.GetAnimator().SetAnimaion(StringDefine.IDLE_ANI_HASH, false);
    }
}

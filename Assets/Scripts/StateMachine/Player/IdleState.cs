using UnityEngine;

public class IdleState : BaseState
{
    public IdleState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

    public override void OnStateEnter()
    {
        Utility.SetAnimaion(player.GetAnimator(),StringDefine.IDLE_ANI_HASH, true);
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
        Utility.SetAnimaion(player.GetAnimator(),StringDefine.IDLE_ANI_HASH, false);
    }
}

using UnityEngine;

public class IdleState : BaseState
{
    public IdleState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

    public override void OnStateEnter()
    {
        player.GetAnimator().SafeSetAnimaion(StringDefine.IDLE_ANI_HASH, true);
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

        else if(player.CheckSkill())
        {
            playerStateMachine.ChangeState(PlayerStateType.Skill);
        }
    }

    public override void OnStateExit()
    {
        player.GetAnimator().SafeSetAnimaion(StringDefine.IDLE_ANI_HASH, false);
    }
}

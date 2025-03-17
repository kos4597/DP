using UnityEngine;

public class SkillState : BaseState
{
    public SkillState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

    public override void OnStateEnter()
    {
        player.GetAnimator().SafeSetAnimaion(StringDefine.SKILL_ANI_HASH);
    }
    public override void OnStateUpdate()
    {
        if (player.AttackAniEndFlag)
        {
            playerStateMachine.ChangeState(PlayerStateType.Idle);
        }
    }

    public override void OnStateExit()
    {
        player.AttackEnd(false);
    }

}

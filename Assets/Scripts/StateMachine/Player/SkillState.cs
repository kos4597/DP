using UnityEngine;

public class SkillState : BaseState
{
    public SkillState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

    public override void OnStateEnter()
    {
        player.GetAnimator().SafeSetAnimaion(StringDefine.SKILL_ANI_HASH);
        CrateSkill();
    }
    public override void OnStateUpdate()
    {
        if (player.AttackAniEndFlag)
        {
            playerStateMachine.ChangeState(PlayerStateType.Idle);
        }
    }

    private void CrateSkill()
    {
        IngameManager.Instance.skillSet.TryGetValue("1", out SkillData skillData);

        if (skillData == null)
            return;

        Skill skill = ResourceManager.CreateGameObject($"{StringDefine.SKILL_FOLDER_PATH}/{skillData.ResourceName}", player.SkillTr.position, Quaternion.identity).GetComponent<Skill>();
        skill.SetSkill(IngameManager.Instance.monsterPool[0], skillData);
    }

    public override void OnStateExit()
    {
        player.AttackEnd(false);
    }

}

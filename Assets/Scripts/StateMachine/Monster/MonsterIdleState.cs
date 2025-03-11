using UnityEngine;

public class MonsterIdleState : BaseState
{
    public MonsterIdleState(Monster monster, MonsterStateMachine stateMachine) : base(monster, stateMachine) { }

    private float doPatrolTime = 0;
    public override void OnStateEnter()
    {
        doPatrolTime = 0f;
        monster.GetComponent<Animator>().SafeSetAnimaion(StringDefine.MONSTER_IDLE_ANI_HASH, true);
    }

    public override void OnStateUpdate()
    {
        doPatrolTime += Time.deltaTime;

        if(doPatrolTime > 2f)
        {
            monsterStateMachine?.ChangeState(MonsterStateType.Patrol);
        }
    }

    public override void OnStateExit()
    {
        monster.GetComponent<Animator>().SafeSetAnimaion(StringDefine.MONSTER_IDLE_ANI_HASH, false);
    }
}

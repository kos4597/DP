using TMPro;
using UnityEngine;

public class DeadState : BaseState
{
    public DeadState(Monster monster, MonsterStateMachine stateMachine) : base(monster, stateMachine) { }

    private float DeadAniTime = 0;
    public override void OnStateEnter()
    {
        monster.GetComponent<Animator>().SafePlayAnimation(StringDefine.DEAD_ANI);
    }

    public override void OnStateUpdate()
    {
        DeadAniTime += Time.deltaTime;

        if(DeadAniTime > 3f)
        {
            monster.Dead();
        }
    }

    public override void OnStateExit()
    {
        monster.GetComponent<Animator>().SafeSetAnimaion(StringDefine.PATROL_ANI_HASH, true);
    }
}

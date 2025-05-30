using TMPro;
using UnityEngine;

public class TrackingState : BaseState
{
    public TrackingState(Monster monster, MonsterStateMachine stateMachine) : base(monster, stateMachine) { }

    private float trackingTime = 0f;
    public override void OnStateEnter()
    {
        monster.GetComponent<Animator>().SafeSetAnimaion(StringDefine.TRACKING_ANI_HASH, true);
        trackingTime = 0f;
    }

    public override void OnStateUpdate()
    {
        trackingTime += Time.deltaTime;

        if(trackingTime > 10f)
        {
            Debug.Log("Change State [RunAway]");
            monsterStateMachine?.ChangeState(MonsterStateType.RunAway);
        }

        else if (monster.CheckPlayerAttackRange())
        {
            monsterStateMachine?.ChangeState(MonsterStateType.Attack);
        }

        else
        {
            Tracking();
        }
    }

    public override void OnStateExit()
    {
        monster.GetComponent<Animator>().SafeSetAnimaion(StringDefine.TRACKING_ANI_HASH, false);
    }

    private void Tracking()
    {
        monster.Agent.SetDestination(monster.TrackingTargetTr.position);       
    }
}

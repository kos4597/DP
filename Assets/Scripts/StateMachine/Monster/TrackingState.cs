using TMPro;
using UnityEngine;

public class TrackingState : BaseState
{
    public TrackingState(Monster monster, MonsterStateMachine stateMachine) : base(monster, stateMachine) { }

    private float trackingTime = 0f;
    public override void OnStateEnter()
    {
        monster.GetComponent<Animator>().SafeSetAnimaion(StringDefine.TRACKING_ANI_HASH, true);
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
        Vector3 direction = (monster.TrackingTargetTr.position - monster.transform.position).normalized;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            monster.transform.rotation = Quaternion.Slerp(monster.transform.rotation, lookRotation, monster.MonsterSO.MonsterData.RotationSpeed * Time.deltaTime);
        }

        monster.Controller.Move(direction * monster.MonsterSO.MonsterData.MoveSpeed * Time.deltaTime);
    }
}

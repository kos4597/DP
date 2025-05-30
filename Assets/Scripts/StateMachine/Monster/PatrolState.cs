using TMPro;
using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class PatrolState : BaseState
{
    public PatrolState(Monster monster, MonsterStateMachine stateMachine) : base(monster, stateMachine) { }

    private Vector3 targetPosition;

    public override void OnStateEnter()
    {
        SetRandomDestination();
        monster.GetComponent<Animator>().SafeSetAnimaion(StringDefine.PATROL_ANI_HASH, true);
    }

    public override void OnStateUpdate()
    {
        if (monster.CheckPlayerInRange())
        {
            monsterStateMachine?.ChangeState(MonsterStateType.Tracking);
        }
        else
        {
            Patrol();
        }
    }

    public override void OnStateExit()
    {
        monster.GetComponent<Animator>().SafeSetAnimaion(StringDefine.PATROL_ANI_HASH, false);
    }

    private void Patrol()
    {
        monster.Agent.SetDestination(targetPosition);

        if (Vector3.Distance(monster.transform.position, targetPosition) <= monster.Agent.stoppingDistance)
        {
            monsterStateMachine.ChangeState(MonsterStateType.Idle);
        }
    }

    private void SetRandomDestination()
    {
        Vector2 randomCircle = Random.insideUnitCircle * monster.MonsterSO.MonsterData.PatrolRange;
        targetPosition = monster.transform.position + new Vector3(randomCircle.x, 0, randomCircle.y);
        targetPosition.y = 0;
    }
}

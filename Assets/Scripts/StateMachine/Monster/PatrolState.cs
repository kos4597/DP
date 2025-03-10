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
            Debug.Log("Change State [Tracking]");
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
        Vector3 direction = (targetPosition - monster.transform.position).normalized;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            monster.transform.rotation = Quaternion.Slerp(monster.transform.rotation, lookRotation, monster.MonsterSO.MonsterData.RotationSpeed * Time.deltaTime);
        }

        monster.Controller.Move(direction * monster.MonsterSO.MonsterData.MoveSpeed * Time.deltaTime);

        Debug.LogError($"Distance : {Vector3.Distance(monster.transform.position, targetPosition)}");

        if (Vector3.Distance(monster.transform.position, targetPosition) < 2f)
        {
            SetRandomDestination();
        }
    }

    private void SetRandomDestination()
    {
        Vector2 randomCircle = Random.insideUnitCircle * monster.MonsterSO.MonsterData.PatrolRange;
        targetPosition = monster.transform.position + new Vector3(randomCircle.x, 0, randomCircle.y);
        targetPosition.y = 0;
    }
}

using TMPro;
using UnityEngine;

public class RunAwayState : BaseState
{
    public RunAwayState(Monster monster, MonsterStateMachine stateMachine) : base(monster, stateMachine) { }

    private Vector3 targetPosition;
    private CharacterController characterController;

    public override void OnStateEnter()
    {
        monster.GetComponent<Animator>().SafeSetAnimaion(StringDefine.RUNAWAY_ANI_HASH);
        targetPosition = monster.SpawnPoint.position;
        characterController = monster.GetComponent<CharacterController>();
    }

    public override void OnStateUpdate()
    {
        if(monster.CheckRunAwayEnd())
        {
            Debug.Log("RunAway End");
            monsterStateMachine.ChangeState(MonsterStateType.Patrol);
        }
        else
        {
            RunAway();
        }
    }

    private void RunAway()
    {
        Vector3 direction = (targetPosition - monster.transform.position).normalized;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            monster.transform.rotation = Quaternion.Slerp(monster.transform.rotation, lookRotation, monster.MonsterSO.MonsterData.RotationSpeed * Time.deltaTime);
        }

        characterController.Move(direction * monster.MonsterSO.MonsterData.MoveSpeed * Time.deltaTime);
    }

    public override void OnStateExit()
    {
        monster.GetComponent<Animator>().SafeSetAnimaion(StringDefine.PATROL_ANI_HASH, true);
    }
}

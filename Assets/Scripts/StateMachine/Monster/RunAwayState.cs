using TMPro;
using UnityEngine;

public class RunAwayState : BaseState
{
    public RunAwayState(Monster monster, MonsterStateMachine stateMachine) : base(monster, stateMachine) { }

    private Vector3 targetPosition;
    private CharacterController characterController;

    public override void OnStateEnter()
    {
        monster.GetComponent<Animator>().SetAnimaion(StringDefine.RUNAWAY_ANI_HASH);
        targetPosition = monster.SpawnPoint.position;
        characterController = monster.GetComponent<CharacterController>();
    }

    public override void OnStateUpdate()
    {
        if(monster.CheckRunAwayEnd())
        {
            Debug.Log("Change State [Patrol]");
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
            monster.transform.rotation = Quaternion.Slerp(monster.transform.rotation, lookRotation, monster.rotationSpeed * Time.deltaTime);
        }

        characterController.Move(direction * monster.speed * Time.deltaTime);
    }

    public override void OnStateExit()
    {
        monster.GetComponent<Animator>().SetAnimaion(StringDefine.PATROL_ANI_HASH, true);
    }
}

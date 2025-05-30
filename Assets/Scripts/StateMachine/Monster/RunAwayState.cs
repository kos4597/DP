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
        monster.Agent.SetDestination(targetPosition);
    }

    public override void OnStateExit()
    {
        monster.GetComponent<Animator>().SafeSetAnimaion(StringDefine.PATROL_ANI_HASH, true);
    }
}

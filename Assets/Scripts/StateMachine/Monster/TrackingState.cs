using UnityEngine;

public class TrackingState : BaseState
{
    public TrackingState(Monster monster, MonsterStateMachine stateMachine) : base(monster, stateMachine) { }

    private float trackingTime = 0f;
    public override void OnStateEnter()
    {
        monster.GetComponent<Animator>().SetAnimaion(StringDefine.TRACKING_ANI_HASH, true);
    }

    public override void OnStateUpdate()
    {
        trackingTime += Time.deltaTime;

        if(trackingTime > 5f)
        {
            Debug.Log("Change State [RunAway]");
            monsterStateMachine.ChangeState(MonsterStateType.RunAway);
        }
    }

    public override void OnStateExit()
    {
        monster.GetComponent<Animator>().SetAnimaion(StringDefine.TRACKING_ANI_HASH, false);
    }
}

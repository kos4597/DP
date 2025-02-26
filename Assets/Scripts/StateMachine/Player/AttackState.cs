using UnityEngine;

public class AttackState : BaseState
{
    public AttackState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

    private int attackStack = -1;
    private float lastAttackTime = 0;

    public override void OnStateEnter()
    {
        InitStack();
        Attack();
    }

    public override void OnStateUpdate()
    {
        if (player.AttackAniEndFlag)
        {
            playerStateMachine.ChangeState(PlayerStateType.Idle);
        }

        else if (Time.time - lastAttackTime > player.PlayerSO.PlayerData.AttackDelayTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Attack();
                lastAttackTime = Time.time;
            }
        }

    }

    public override void OnStateExit()
    {
        InitStack();
        Debug.Log("Attack Exit");
        player.AttackEnd(false);
    }

    private void InitStack()
    {
        attackStack = -1;
        lastAttackTime = 0;
    }
    private void Attack()
    {
        attackStack++;

        Debug.Log("stack : " + attackStack);
        Utility.SetAnimaion(player.GetAnimator(), StringDefine.ATTACKSTACK_ANI_HASH, attackStack);
        Utility.SetAnimaion(player.GetAnimator(), StringDefine.ATTACK_ANI_HASH);
    }
}

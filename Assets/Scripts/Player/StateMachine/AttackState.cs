using UnityEngine;

public class AttackState : BaseState
{
    public AttackState(Player player) : base(player) { }

    private int attackStack = -1;
    private float lastAttackTime = 0;

    public override void OnStateEnter()
    {
        InitStack();
        Attack();
    }

    public override void OnStateUpdate()
    {
        if (Time.time - lastAttackTime > player.PlayerSO.PlayerData.AttackDelayTime)
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
        player.SetAnimaion(StringDefine.AttackStackAniHash, attackStack);
        player.SetAnimaion(StringDefine.AttackAniHash);
    }
}

using UnityEngine;

public class IdleState : BaseState
{
    public IdleState(Player player) : base(player) { }

    public override void OnStateEnter()
    {
        player.SetAnimaion(StringDefine.IdleAniHash, true);
    }

    public override void OnStateUpdate()
    {

    }

    public override void OnStateExit()
    {
        player.SetAnimaion(StringDefine.IdleAniHash, false);
    }
}

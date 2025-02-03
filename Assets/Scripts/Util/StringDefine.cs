using UnityEngine;

public static class StringDefine
{
    public readonly static int IdleAniHash = Animator.StringToHash("Idle");
    public readonly static int MoveSpeedAniHash = Animator.StringToHash("MoveSpeed");
    public readonly static int RunAniHash = Animator.StringToHash("Run");
    public readonly static int JumpAniHash = Animator.StringToHash("Jump");
    public readonly static int AttackAniHash = Animator.StringToHash("Attack");
    public readonly static int AttackStackAniHash = Animator.StringToHash("AttackStack");

    public readonly static string ComboAttack01Ani = "Attack1";
    public readonly static string ComboAttack02Ani = "Attack2";
    public readonly static string ComboAttack03Ani = "Attack3";
}

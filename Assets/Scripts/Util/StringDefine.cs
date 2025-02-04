using UnityEngine;

public static class StringDefine
{
    /// <summary>
    /// const, readonly ���� �빮�ڷθ� ���̹�
    /// IdleAniHash -> IDLE_ANI_HASH or IDLE_ANIMATION_HASH
    /// </summary>
    public readonly static int IDLE_ANI_HASH = Animator.StringToHash("Idle");
    public readonly static int MOVESPEED_ANI_HASH = Animator.StringToHash("MoveSpeed");
    public readonly static int RUN_ANI_HASH = Animator.StringToHash("Run");
    public readonly static int JUMP_ANI_HASH = Animator.StringToHash("Jump");
    public readonly static int ATTACK_ANI_HASH = Animator.StringToHash("Attack");
    public readonly static int ATTACKSTACK_ANI_HASH = Animator.StringToHash("AttackStack");

    public readonly static string COMBOATTACK_01_ANI = "Attack1";
    public readonly static string COMBOATTACK_02_ANI = "Attack2";
    public readonly static string COMBOATTACK_03_ANI = "Attack3";
}

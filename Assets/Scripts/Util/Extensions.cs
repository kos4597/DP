using System;
using UnityEngine;

/// <summary>
/// ���� Ȯ��޼ҵ� ���̹��� SafeSet���� ����
/// </summary>
public static class Extensions
{
    /// <summary>
    /// Trigger Param
    /// </summary>
    /// <param name="hash"></param>
    public static void SetAnimaion(this Animator animator, int hash)
    {
        // animator == null�� missing�� ��� null�� �ƴ� "null"�̶� ���� �߻���
        // if (animator == null || animator.Equals(null))�� �ϴ°� ����
        // �� null üũ�� �Լ��� ���� ���� �ϴ°͵� ����
        //public static bool CheckSafeNull(this Object obj)
        //{
        //    return obj == null || obj.Equals(null);
        //}

        if (animator == null)
            return;

        animator.SetTrigger(hash);
    }
    /// <summary>
    /// Bool param
    /// </summary>
    /// <param name="hash"></param>
    /// <param name="isOn"></param>
    public static void SetAnimaion(this Animator animator, int hash, bool isOn)
    {
        if (animator == null)
            return;

        animator.SetBool(hash, isOn);
    }
    /// <summary>
    /// Int Param
    /// </summary>
    /// <param name="hash"></param>
    /// <param name="count"></param>
    public static void SetAnimaion(this Animator animator, int hash, int count)
    {
        if (animator == null)
            return;

        animator.SetInteger(hash, count);
    }
    /// <summary>
    /// Float Param
    /// </summary>
    /// <param name="hash"></param>
    /// <param name="count"></param>
    public static void SetAnimaion(this Animator animator, int hash, float count)
    {
        if (animator == null)
            return;

        animator.SetFloat(hash, count);
    }


    public static float GetAnimFloatParam(this Animator animator, int hash)
    {
        return animator.GetFloat(hash);
    }
}

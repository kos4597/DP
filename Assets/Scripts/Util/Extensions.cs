using System;
using UnityEngine;

/// <summary>
/// 보통 확장메소드 네이밍은 SafeSet으로 시작
/// </summary>
public static class Extensions
{
    /// <summary>
    /// Trigger Param
    /// </summary>
    /// <param name="hash"></param>
    public static void SetAnimaion(this Animator animator, int hash)
    {
        // animator == null은 missing인 경우 null이 아닌 "null"이라서 에러 발생함
        // if (animator == null || animator.Equals(null))로 하는게 좋고
        // 이 null 체크도 함수를 만들어서 관리 하는것도 좋음
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

using System;
using UnityEngine;

public static class Extensions
{
    /// <summary>
    /// Trigger Param
    /// </summary>
    /// <param name="hash"></param>
    public static void SafeSetAnimaion(this Animator animator, int hash)
    {
        if (CheckFakeNull(animator))
            return;

        animator.SetTrigger(hash);
    }
    /// <summary>
    /// Bool param
    /// </summary>
    /// <param name="hash"></param>
    /// <param name="isOn"></param>
    public static void SafeSetAnimaion(this Animator animator, int hash, bool isOn)
    {
        if (CheckFakeNull(animator))
            return;

        animator.SetBool(hash, isOn);
    }
    /// <summary>
    /// Int Param
    /// </summary>
    /// <param name="hash"></param>
    /// <param name="count"></param>
    public static void SafeSetAnimaion(this Animator animator, int hash, int count)
    {
        if (CheckFakeNull(animator))
            return;

        animator.SetInteger(hash, count);
    }
    /// <summary>
    /// Float Param
    /// </summary>
    /// <param name="hash"></param>
    /// <param name="count"></param>
    public static void SafeSetAnimaion(this Animator animator, int hash, float count)
    {
        if (CheckFakeNull(animator))
            return;

        animator.SetFloat(hash, count);
    }


    public static float SafeGetAnimFloatParam(this Animator animator, int hash)
    {
        if (CheckFakeNull(animator))
            return 0;

        return animator.GetFloat(hash);
    }

    public static void SafePlayAnimation(this Animator animator, string name)
    {
        if (CheckFakeNull(animator))
            return;

        animator.Play(name);
    }

    public static bool CheckFakeNull(this UnityEngine.Object obj)
    {
        return obj == null || obj.Equals(null);
    }
}

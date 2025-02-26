using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Utility : MonoBehaviour
{
    public enum FadeType
    {
        FadeIn,
        FadeOut,
    }

    public static void FadeInOut(RawImage texture = null, TMP_Text text = null, FadeType type = FadeType.FadeIn, float fadeTime = 1f)
    {
        switch(type)
        {
            case FadeType.FadeIn:
                {
                    if(texture != null)
                        texture.CrossFadeAlpha(0f, fadeTime, false);

                    if(text != null)
                        text.CrossFadeAlpha(0f,fadeTime, false);
                }
                break;

            case FadeType.FadeOut:
                {
                    if (texture != null)
                        texture.CrossFadeAlpha(1f, fadeTime, false);

                    if (text != null)
                        text.CrossFadeAlpha(1f, fadeTime, false);
                }
                break;
        }
    }

    /// <summary>
    /// Trigger Param
    /// </summary>
    /// <param name="hash"></param>
    public static void SetAnimaion(Animator animator, int hash)
    {
        if (animator == null)
            return;

        animator.SetTrigger(hash);
    }
    /// <summary>
    /// Bool param
    /// </summary>
    /// <param name="hash"></param>
    /// <param name="isOn"></param>
    public static void SetAnimaion(Animator animator, int hash, bool isOn)
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
    public static void SetAnimaion(Animator animator,int hash, int count)
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
    public static void SetAnimaion(Animator animator,int hash, float count)
    {
        if (animator == null)
            return;

        animator.SetFloat(hash, count);
    }


    public static float GetAnimFloatParam(Animator animator,int hash)
    {
        return animator.GetFloat(hash);
    }
}

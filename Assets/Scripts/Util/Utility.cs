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
}

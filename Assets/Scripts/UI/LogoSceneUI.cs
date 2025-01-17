using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LogoSceneUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text logoText = null;
    [SerializeField]
    private RawImage logoTexture = null;
    private void Awake()
    {
        logoText.canvasRenderer.SetAlpha(0f);
        logoTexture.canvasRenderer.SetAlpha(0f);
    }

    private void Start()
    {
        LoadLogo();
    }

    private void LoadLogo()
    {
        float fadeTime = 3f;
        Utility.FadeInOut(logoTexture, logoText, Utility.FadeType.FadeOut, fadeTime);
    }
}

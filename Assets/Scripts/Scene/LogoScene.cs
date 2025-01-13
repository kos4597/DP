using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LogoScene : SceneBase
{
    [SerializeField]
    private TMP_Text logoText = null;
    [SerializeField]
    private RawImage logoTexture = null;

    private Coroutine logoCor = null;

    private void Awake()
    {
        logoText.canvasRenderer.SetAlpha(0f);
        logoTexture.canvasRenderer.SetAlpha(0f);

    }

    private void Start()
    {
        EnterScene();
    }

    public override void EnterScene()
    {
        float fadeTime = 3f;

        logoCor = StartCoroutine(LogoCoroutine(fadeTime));
    }

    public override void ExitScene() 
    {
        if (logoCor != null)
        {
            Debug.Log("StopLogoCor");
            StopCoroutine(logoCor);
            logoCor = null;
        }
    }

    private IEnumerator LogoCoroutine(float fadeTime)
    {
        yield return null;

        WaitForSeconds delay = new WaitForSeconds(2f);

        Utility.FadeInOut(logoTexture, logoText, Utility.FadeType.FadeOut, fadeTime);

        yield return new WaitForSeconds(fadeTime);

        yield return delay;

        SceneChanger.Instance.ChangeScene(SceneChanger.SceneType.Intro, false);
    }

    private void OnDestroy()
    {
        ExitScene();
    }

}

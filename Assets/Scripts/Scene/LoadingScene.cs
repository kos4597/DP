using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static SceneChanger;

public class LoadingScene : SceneBase
{
    [SerializeField]
    private Image progressBar = null;
    [SerializeField]
    private TMP_Text progressText = null;

    private Coroutine changeCor = null;

    private void Awake()
    {
        InitUI();
    }

    private void Start()
    {
        EnterScene();
    }

    public override void EnterScene()
    {
        if (changeCor != null)
        {
            StopCoroutine(changeCor);
            changeCor = null;
        }

        changeCor = StartCoroutine(ChangeSceneAfterLoadingCor());
    }

    private void InitUI()
    {
        progressBar.fillAmount = 0f;
        progressText.text = "0f";
    }

    public override void ExitScene() 
    {
        if (changeCor != null)
        {
            StopCoroutine(changeCor);
            changeCor = null;
        }
    }

    public IEnumerator ChangeSceneAfterLoadingCor()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync($"{SceneChanger.Instance.NextSceneType}");

        float timer = 0.0f;
        asyncOperation.allowSceneActivation = false;

        while (asyncOperation.isDone == false)
        {
            Debug.Log($"Scene Load AfterLoading : + {asyncOperation.progress * 100}%");

            yield return null;
            timer += Time.deltaTime;
            if (asyncOperation.progress < 0.9f)
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, asyncOperation.progress, timer);
                progressText.text = $"{asyncOperation.progress * 100}%";
                if (progressBar.fillAmount >= asyncOperation.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer);
                if (progressBar.fillAmount == 1.0f)
                {
                    progressText.text = $"100%";
                    yield return new WaitForSeconds(2f);
                    asyncOperation.allowSceneActivation = true;
                    yield break;
                }
            }
        }

        SceneChanger.Instance.NowScene = SceneChanger.Instance.NextSceneType switch
        {
            SceneType.Logo => new LogoScene(),
            SceneType.Intro => new IntroScene(),
            SceneType.Lobby => new LobbyScene(),
            SceneType.Ingame => new IntroScene(),
            _ => null
        };
    }

    private void OnDestroy()
    {
        ExitScene();
    }

}

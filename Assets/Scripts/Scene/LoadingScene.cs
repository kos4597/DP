using Cysharp.Threading.Tasks;
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
       
    }

    private void Start()
    {
        EnterScene();
    }

    public override void EnterScene()
    {
        this.sceneState = SceneState.Enter;
        InitUI();
    }

    public override async UniTask LoadingSceneAsync()
    {
        this.sceneState = SceneState.Loading;
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync($"{SceneChanger.Instance.CurrentScene.sceneType}");

        float timer = 0.0f;
        asyncOperation.allowSceneActivation = false;

        while (asyncOperation.isDone == false)
        {
            Debug.Log($"Scene Load AfterLoading : + {asyncOperation.progress * 100}%");

            await UniTask.NextFrame();

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
                    await UniTask.Delay(2000);
                    asyncOperation.allowSceneActivation = true;
                    break;
                }
            }
        }

        SceneChanger.Instance.ChangeScene(SceneChanger.Instance.CurrentScene.sceneType, false);
    }

    public override void UpdateScene()
    {
        this.sceneState = SceneState.Update;
    }

    public override async UniTask ExitScene()
    {
        await base.ExitScene();
    }


    private void InitUI()
    {
        progressBar.fillAmount = 0f;
        progressText.text = "0f";
    }
}

using Cysharp.Threading.Tasks;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogoScene : SceneBase
{
    public override void EnterScene()
    {
        this.sceneState = SceneChanger.SceneState.Enter;
    }

    public override async UniTask LoadingSceneAsync()
    {
        this.sceneState = SceneChanger.SceneState.Loading;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync($"{this.sceneType}");

        while (asyncOperation.isDone == false)
        {
            Debug.Log($"Logo Scene Load : + {asyncOperation.progress * 100}%");

            await UniTask.NextFrame();
        }

        await UniTask.Delay(4000);

        this.sceneState = SceneChanger.SceneState.Update;
    }

    public override void UpdateScene()
    {
        SceneChanger.Instance.ExitScene();
    }

    public override void ExitScene()
    {
        SceneChanger.SceneType next = SceneChanger.SceneType.Intro;

        SceneChanger.Instance.ChangeScene(next, false).Forget();
    }
}

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
        ChangeSceneState(SceneChanger.SceneState.Enter);
    }

    public override async UniTask LoadingSceneAsync()
    {
        ChangeSceneState(SceneChanger.SceneState.Loading);

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync($"{this.SceneType}");

        while (asyncOperation.isDone == false)
        {
            Debug.Log($"Logo Scene Load : + {asyncOperation.progress * 100}%");

            await UniTask.NextFrame();
        }

        await UniTask.Delay(4000);

        ChangeSceneState(SceneChanger.SceneState.Update);
    }

    public override void UpdateScene()
    {
        ChangeSceneState(SceneChanger.SceneState.Exit);
    }

    public override void ExitScene()
    {
        SceneChanger.SceneType next = SceneChanger.SceneType.Intro;

        SceneChanger.Instance.ChangeScene(next).Forget();
    }
}

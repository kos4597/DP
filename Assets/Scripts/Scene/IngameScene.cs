using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IngameScene : SceneBase
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

        this.sceneState = SceneChanger.SceneState.Update;
    }

    public override void UpdateScene()
    {
        SceneChanger.Instance.ExitScene();
    }

    public override async UniTask ExitScene()
    {
        await base.ExitScene();
    }
}

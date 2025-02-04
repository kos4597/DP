using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : SceneBase
{
    public override void EnterScene()
    {
        ChangeSceneState(SceneChanger.SceneState.Enter);
    }

    public override async UniTask LoadingSceneAsync()
    {
        ChangeSceneState(SceneChanger.SceneState.Loading);

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync($"{SceneChanger.Instance.CurrentScene.sceneType}");

        while (asyncOperation.isDone == false)
        {
            Debug.Log($"Scene Load AfterLoading : + {asyncOperation.progress * 100}%");

            if(LoadingSceneManager.Instance != null)
            {
                LoadingSceneManager.Instance.SetLoadingProgressUI(asyncOperation.progress);

                if (LoadingSceneManager.Instance.IsLoadingEnd)
                {
                    asyncOperation.allowSceneActivation = true;
                    break;
                }
            }
                
            await UniTask.NextFrame();
        }

        ChangeSceneState(SceneChanger.SceneState.Update);
    }

    public override void UpdateScene()
    {
        ChangeSceneState(SceneChanger.SceneState.Exit);
    }

    public override void ExitScene()
    {
        SceneChanger.Instance.ChangeScene(SceneChanger.Instance.PrevScene.sceneType, false).Forget();
    }
}

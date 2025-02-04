using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyScene : SceneBase
{
    public override void EnterScene()
    {
        ChangeSceneState(SceneChanger.SceneState.Enter);
        Debug.Log("Lobby Setting");
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

        ChangeSceneState(SceneChanger.SceneState.Update);
    }

    public override void UpdateScene()
    {
        ChangeSceneState(SceneChanger.SceneState.Exit);
    }

    public override void ExitScene()
    {

    }
}

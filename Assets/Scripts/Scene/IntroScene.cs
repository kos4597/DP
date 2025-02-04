using System.Collections;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroScene : SceneBase
{
    public override void EnterScene()
    {
        ChangeSceneState(SceneChanger.SceneState.Enter);
    }

    public override async UniTask LoadingSceneAsync()
    {
        ChangeSceneState(SceneChanger.SceneState.Loading);

        // 리소스 관리 매니저를 하나 만들어서 Scene이든 Texture든 해당 매니저에서 불러오도록 할 것.
        // 예) ResourcesManager
        // await ResourcesManager.LoadAsset<Texture>(path);
        // await ResourcesManager.LoadScene(path, LoadSceneMode.Single);
        // 나중에는 ResourcesManager가 로드한 리소스를 핸들링해서 로비나 전투 진입 전에 메모리를 털어주거나 해야 함

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

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

        // ���ҽ� ���� �Ŵ����� �ϳ� ���� Scene�̵� Texture�� �ش� �Ŵ������� �ҷ������� �� ��.
        // ��) ResourcesManager
        // await ResourcesManager.LoadAsset<Texture>(path);
        // await ResourcesManager.LoadScene(path, LoadSceneMode.Single);
        // ���߿��� ResourcesManager�� �ε��� ���ҽ��� �ڵ鸵�ؼ� �κ� ���� ���� ���� �޸𸮸� �о��ְų� �ؾ� ��

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

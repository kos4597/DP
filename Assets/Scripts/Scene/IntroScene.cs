using Cysharp.Threading.Tasks;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroScene : SceneBase
{
    public override void EnterScene()
    {
        this.sceneState = SceneChanger.SceneState.Enter;
    }

    public override async UniTask LoadingSceneAsync()
    {
        this.sceneState = SceneChanger.SceneState.Loading;
    }

    public override void UpdateScene()
    {

    }

    public override async UniTask ExitScene()
    {
        await base.ExitScene();
    }
}

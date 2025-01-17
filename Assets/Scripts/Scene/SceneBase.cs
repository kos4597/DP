using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBase
{
    public SceneChanger.SceneType sceneType { get; set; }
    public SceneChanger.SceneState sceneState { get; set; }

    public virtual void EnterScene() { }
    public virtual async UniTask LoadingSceneAsync() { }
    public virtual void UpdateScene() { }
    public virtual async UniTask ExitScene() 
    {
        this.sceneState = SceneChanger.SceneState.Exit;
    }

}
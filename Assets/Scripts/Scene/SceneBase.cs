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

    /// <summary>
    /// 다른씬으로 이동시 지워줄 것들 처리.
    /// </summary>
    public virtual void ExitScene() {}

    public virtual void ChangeSceneState(SceneChanger.SceneState state)
    {
        this.sceneState = state;
    }

}
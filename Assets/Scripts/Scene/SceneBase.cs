using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBase
{
    public SceneChanger.SceneType SceneType { get; private set; }
    public SceneChanger.SceneState SceneState { get; private set; }

    public virtual void EnterScene() { }
    public virtual async UniTask LoadingSceneAsync() { }
    public virtual void UpdateScene() { }

    /// <summary>
    /// 다른씬으로 이동시 지워줄 것들 처리.
    /// </summary>
    public virtual void ExitScene() { }

    public virtual void ChangeSceneType(SceneChanger.SceneType type)
    {
        this.SceneType = type;
    }

    public virtual void ChangeSceneState(SceneChanger.SceneState state)
    {
        this.SceneState = state;
    }

}
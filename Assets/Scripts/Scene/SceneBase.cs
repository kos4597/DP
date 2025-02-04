using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBase
{
    // 클래스,함수,프로퍼티는 파스칼케이스, 벨류는 카멜케이스로 작성할 것
    // 프로퍼티에 되도록이면 public set은 하지 말 것

    public SceneChanger.SceneType sceneType { get; set; }
    public SceneChanger.SceneState sceneState { get; set; }

    public virtual void EnterScene() { }
    public virtual async UniTask LoadingSceneAsync() { }
    public virtual void UpdateScene() { }

    /// <summary>
    /// 다른씬으로 이동시 지워줄 것들 처리.
    /// </summary>
    public virtual void ExitScene() { }

    public virtual void ChangeSceneState(SceneChanger.SceneState state)
    {
        this.sceneState = state;
    }

}
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBase
{
    // Ŭ����,�Լ�,������Ƽ�� �Ľ�Į���̽�, ������ ī�����̽��� �ۼ��� ��
    // ������Ƽ�� �ǵ����̸� public set�� ���� �� ��

    public SceneChanger.SceneType sceneType { get; set; }
    public SceneChanger.SceneState sceneState { get; set; }

    public virtual void EnterScene() { }
    public virtual async UniTask LoadingSceneAsync() { }
    public virtual void UpdateScene() { }

    /// <summary>
    /// �ٸ������� �̵��� ������ �͵� ó��.
    /// </summary>
    public virtual void ExitScene() { }

    public virtual void ChangeSceneState(SceneChanger.SceneState state)
    {
        this.sceneState = state;
    }

}
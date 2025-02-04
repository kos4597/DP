using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBase
{
    // Ŭ����,�Լ�,������Ƽ�� �Ľ�Į���̽�, ������ ī�����̽��� �ۼ��� ��
    // ������Ƽ�� �ǵ����̸� public set�� ���� �� ��

    public SceneChanger.SceneType SceneType { get; private set; }
    public SceneChanger.SceneState SceneState { get; private set; }

    public CancellationTokenSource Can = new CancellationTokenSource();


    public virtual void EnterScene() { }
    public virtual async UniTask LoadingSceneAsync() { }
    public virtual void UpdateScene() { }

    /// <summary>
    /// �ٸ������� �̵��� ������ �͵� ó��.
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
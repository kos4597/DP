using System;
using UnityEngine;

public class LoadingSceneManager : MonoBehaviour
{
    public static LoadingSceneManager Instance { get; private set; }

    [SerializeField]
    private LoadingSceneUI loadingSceneUI = null;

    public bool IsLoadingEnd {  get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    // 클래스든 함수든 뭐든간에 단일원칙을 지킬 것.
    // 역할은 분명해야 함
    public void SetLoadingProgressUI(float amount)
    {
        loadingSceneUI.SetFillAmount(amount, () => { IsLoadingEnd = true; });
    }
}

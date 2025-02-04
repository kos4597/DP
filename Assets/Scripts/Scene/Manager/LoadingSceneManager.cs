using System;
using UnityEngine;

public class LoadingSceneManager : MonoBehaviour
{
    public static LoadingSceneManager Instance { get; private set; }

    [SerializeField]
    private LoadingSceneUI loadingSceneUI = null;

    // 왜 퍼블릭임?
    public bool isLoadingEnd = false;
    private void Awake()
    {
        Instance = this;
    }

    // 클래스든 함수든 뭐든간에 단일원칙을 지킬 것.
    // 역할은 분명해야 함
    public bool SetLoadingProgressUI(float amount)
    {
        loadingSceneUI.SetFillAmount(amount, () => { isLoadingEnd = true; });
        return isLoadingEnd;
    }
}

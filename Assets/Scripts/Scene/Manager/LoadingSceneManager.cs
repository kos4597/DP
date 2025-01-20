using System;
using UnityEngine;

public class LoadingSceneManager : MonoBehaviour
{
    public static LoadingSceneManager Instance { get; private set; }

    [SerializeField]
    private LoadingSceneUI loadingSceneUI = null;

    public bool isLoadingEnd = false;
    private void Awake()
    {
        Instance = this;
    }

    public bool SetLoadingProgressUI(float amount)
    {
        loadingSceneUI.SetFillAmount(amount, () => { isLoadingEnd = true; });
        return isLoadingEnd;
    }
}

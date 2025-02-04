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

    // Ŭ������ �Լ��� ���簣�� ���Ͽ�Ģ�� ��ų ��.
    // ������ �и��ؾ� ��
    public void SetLoadingProgressUI(float amount)
    {
        loadingSceneUI.SetFillAmount(amount, () => { IsLoadingEnd = true; });
    }
}

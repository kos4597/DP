using System;
using UnityEngine;

public class LoadingSceneManager : MonoBehaviour
{
    public static LoadingSceneManager Instance { get; private set; }

    [SerializeField]
    private LoadingSceneUI loadingSceneUI = null;

    // �� �ۺ���?
    public bool isLoadingEnd = false;
    private void Awake()
    {
        Instance = this;
    }

    // Ŭ������ �Լ��� ���簣�� ���Ͽ�Ģ�� ��ų ��.
    // ������ �и��ؾ� ��
    public bool SetLoadingProgressUI(float amount)
    {
        loadingSceneUI.SetFillAmount(amount, () => { isLoadingEnd = true; });
        return isLoadingEnd;
    }
}

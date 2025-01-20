using Cysharp.Threading.Tasks;
using System;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingSceneUI : MonoBehaviour
{
    [SerializeField]
    private Image progressBar = null;
    [SerializeField]
    private TMP_Text progressText = null;

    private float timer = 0.0f;

    private void Awake()
    {
        InitUI();
    }

    private void InitUI()
    {
        progressBar.fillAmount = 0f;
        progressText.text = "0f";
    }

    public void SetFillAmount(float amount, Action endCallback = null)
    {
        timer += Time.deltaTime;
        if (amount < 0.9f)
        {
            progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, amount, timer);
            progressText.text = $"{amount * 100}%";
            if (progressBar.fillAmount >= amount)
            {
                timer = 0f;
            }
        }
        else
        {
            progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer);
            if (progressBar.fillAmount == 1.0f)
            {
                progressText.text = $"100%";
                endCallback?.Invoke();
            }
        }
        progressBar.fillAmount = amount;
    }

    public void SetProgressText(float amount)
    {
        progressText.text = $"{amount}";
    }
}

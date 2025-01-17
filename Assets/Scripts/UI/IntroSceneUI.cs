using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntroSceneUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text loadStateText = null;
    [SerializeField]
    private TMP_Text loadStateCount = null;
    [SerializeField]
    private Image progressBar = null;
    [SerializeField]
    private Button btnTouchToPlay = null;

    private void Awake()
    {
        InitUI();
    }

    private void Start()
    {
        LoadData();
    }

    private void InitUI()
    {
        loadStateText.text = "";
        loadStateCount.text = "";
        progressBar.fillAmount = 0f;
        btnTouchToPlay.onClick.AddListener(OnClickTouchToStart);
        btnTouchToPlay.gameObject.SetActive(false);
    }

    private async UniTask LoadData()
    {
        int count = 0;

        while (count < TableManager.Instance.tableList.Count)
        {
            TableManager.Instance.tableList[count].Load();

            loadStateText.text = $"{TableManager.Instance.tableList[count].tableKey} Loading...";
            loadStateCount.text = $"{count + 1}/{TableManager.Instance.tableList.Count}";
            progressBar.fillAmount = (float)count + 1 / TableManager.Instance.tableList.Count;

            Debug.Log($"{count + 1} / {TableManager.Instance.tableList.Count} / {TableManager.Instance.tableList[count].tableKey}");

            await UniTask.NextFrame();
            count++;
        }

        btnTouchToPlay.gameObject.SetActive(true);
    }
    private void OnClickTouchToStart()
    {
        SceneChanger.Instance.ExitScene();
        SceneChanger.Instance.ChangeScene(SceneChanger.SceneType.Lobby, true);
    }
}

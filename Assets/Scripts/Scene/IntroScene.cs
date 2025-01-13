using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntroScene : SceneBase
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
        btnTouchToPlay.onClick.AddListener(OnClickTouchToStart);
    }

    private void Start()
    {
        EnterScene();
    }

    private void InitUI()
    {
        loadStateText.text = "";
        loadStateCount.text = "";
        progressBar.fillAmount = 0f;
        btnTouchToPlay.gameObject.SetActive(false);
    }

    public override void EnterScene()
    {
        StartCoroutine(LoadDataBase());
    }
    public override void ExitScene() 
    {
        
    }

    private IEnumerator LoadDataBase()
    {
        int count = 0;

        while (count < TableManager.Instance.tableList.Count)
        {
            TableManager.Instance.tableList[count].Load();

            loadStateText.text = $"{TableManager.Instance.tableList[count].tableKey} Loading...";
            loadStateCount.text = $"{count+1}/{TableManager.Instance.tableList.Count}";
            progressBar.fillAmount = (float)count+1 / TableManager.Instance.tableList.Count;

            Debug.Log($"{count+1} / {TableManager.Instance.tableList.Count} / {TableManager.Instance.tableList[count].tableKey}");

            yield return null;
            count++;
        }

        btnTouchToPlay.gameObject.SetActive(true);
    }

    private void OnClickTouchToStart()
    {
        SceneChanger.Instance.ChangeScene(SceneChanger.SceneType.Lobby, true);
    }
}

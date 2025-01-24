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
        LoadData().Forget();
    }

    private void InitUI()
    {
        loadStateText.text = "";
        loadStateCount.text = "";
        progressBar.fillAmount = 0f;
        btnTouchToPlay.onClick.AddListener(OnClickTouchToStart);
        btnTouchToPlay.gameObject.SetActive(false);

        // SetActive�� ������ ���� ���Ǵ� �Լ���
        // Ȯ��޼ҵ带 �̿��ؼ� SafeSetActive�� �����ϰ� ���ο��� null�� ���� ����ó���� �� ��
        // ����Ƽ�� Fake Null�� �ֱ� ������ obj == null�� obj.Equals(null) �� ���� üũ ����� ��
        // loadStateText.text = "";�� ���������� Ȯ��޼ҵ�
    }

    private async UniTask LoadData()
    {
        int count = 0;

        // for������ �ص� �Ǵ� ������ while���ؼ� �������� ������
        while (count < TableManager.Instance.tableList.Count)
        {
            TableManager.Instance.tableList[count].Load();

            loadStateText.text = $"{TableManager.Instance.tableList[count].tableKey} Loading...";
            loadStateCount.text = $"{count + 1}/{TableManager.Instance.tableList.Count}";
            progressBar.fillAmount = (float)count + 1 / TableManager.Instance.tableList.Count;

            Debug.Log($"{count + 1} / {TableManager.Instance.tableList.Count} / {TableManager.Instance.tableList[count].tableKey}");

            // UniTask�� �ݵ�� ��ū�� �Բ� ����ϰ� Suppresscancellationthrow�� �ٿ��� ���ο� �߻� �� ������ ���������� ��ȣ �ڵ嵵 �� ���� ��
            // if( await UniTask.NextFrame(��ū).Suppresscancellationthrow()) return; << ���ܰ� �߻��ϸ� ������ ���������� ��
            // await �߿� IntroSceneUI�� null�� �ǰų� �ϸ� ������ �߻��� �� ����
            await UniTask.NextFrame();

            count++;
        }

        btnTouchToPlay.gameObject.SetActive(true);
    }
    private void OnClickTouchToStart()
    {
        SceneChanger.SceneType next = SceneChanger.SceneType.Lobby;

        SceneChanger.Instance.ChangeScene(next, true).Forget();
    }
}

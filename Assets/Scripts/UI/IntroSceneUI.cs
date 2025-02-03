using Cysharp.Threading.Tasks;
using System.Threading;
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

    private CancellationTokenSource cancelToken = new CancellationTokenSource();
    private void Awake()
    {
        InitUI();
    }

    private void Start()
    {
        cancelToken = new CancellationTokenSource();
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
        for(int i = 0; i < TableManager.Instance.tableList.Count; i++)
        {
            TableManager.Instance.tableList[i].Load();

            loadStateText.text = $"{TableManager.Instance.tableList[i].tableKey} Loading...";
            loadStateCount.text = $"{i + 1}/{TableManager.Instance.tableList.Count}";
            progressBar.fillAmount = (float)i + 1 / TableManager.Instance.tableList.Count;

            Debug.Log($"{i + 1} / {TableManager.Instance.tableList.Count} / {TableManager.Instance.tableList[i].tableKey}");

            // UniTask�� �ݵ�� ��ū�� �Բ� ����ϰ� Suppresscancellationthrow�� �ٿ��� ���ο� �߻� �� ������ ���������� ��ȣ �ڵ嵵 �� ���� ��
            // if( await UniTask.NextFrame(��ū).Suppresscancellationthrow()) return; << ���ܰ� �߻��ϸ� ������ ���������� ��
            // await �߿� IntroSceneUI�� null�� �ǰų� �ϸ� ������ �߻��� �� ����
            if (await UniTask.NextFrame(cancellationToken: cancelToken.Token).SuppressCancellationThrow())
                return;
        }

        btnTouchToPlay.gameObject.SetActive(true);
    }
    private void OnClickTouchToStart()
    {
        SceneChanger.SceneType next = SceneChanger.SceneType.Lobby;

        SceneChanger.Instance.ChangeScene(next, true).Forget();
    }

    private void OnDestroy()
    {
        cancelToken.Cancel();
        cancelToken.Dispose();
    }
}

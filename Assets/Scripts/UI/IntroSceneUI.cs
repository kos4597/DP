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

        // SetActive는 굉장히 자주 사용되는 함수임
        // 확장메소드를 이용해서 SafeSetActive를 구현하고 내부에서 null에 대한 예외처리를 할 것
        // 유니티는 Fake Null이 있기 때문에 obj == null과 obj.Equals(null) 두 개로 체크 해줘야 함
        // loadStateText.text = "";도 마찬가지로 확장메소드
    }

    private async UniTask LoadData()
    {
        int count = 0;

        // for문으로 해도 되는 로직을 while로해서 가독성만 떨어짐
        while (count < TableManager.Instance.tableList.Count)
        {
            TableManager.Instance.tableList[count].Load();

            loadStateText.text = $"{TableManager.Instance.tableList[count].tableKey} Loading...";
            loadStateCount.text = $"{count + 1}/{TableManager.Instance.tableList.Count}";
            progressBar.fillAmount = (float)count + 1 / TableManager.Instance.tableList.Count;

            Debug.Log($"{count + 1} / {TableManager.Instance.tableList.Count} / {TableManager.Instance.tableList[count].tableKey}");

            // UniTask는 반드시 토큰과 함께 사용하고 Suppresscancellationthrow를 붙여서 쓰로우 발생 시 로직을 빠져나가는 보호 코드도 꼭 넣을 것
            // if( await UniTask.NextFrame(토큰).Suppresscancellationthrow()) return; << 예외가 발생하면 로직을 빠져나가야 함
            // await 중에 IntroSceneUI가 null이 되거나 하면 에러가 발생할 수 있음
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

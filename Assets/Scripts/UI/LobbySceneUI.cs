using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class LobbySceneUI : MonoBehaviour
{
    [SerializeField]
    private Button btnBattle = null; // 헝가리안표기 방식 사용X

    private void Awake()
    {
        btnBattle.onClick.AddListener(OnClickGoBattle);
    }

    private void OnClickGoBattle()
    {
        SceneChanger.Instance.ChangeScene(SceneChanger.SceneType.Ingame, true).Forget();
    }
}

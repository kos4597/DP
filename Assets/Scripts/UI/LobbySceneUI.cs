using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class LobbySceneUI : MonoBehaviour
{
    [SerializeField]
    private Button battleButton = null;

    private void Awake()
    {
        battleButton.onClick.AddListener(OnClickGoBattle);
    }

    private void OnClickGoBattle()
    {
        SceneChanger.Instance.ChangeScene(SceneChanger.SceneType.Ingame, true).Forget();
    }
}

using UnityEngine;
using UnityEngine.UI;

public class LobbyScene : SceneBase
{
    [SerializeField]
    private Button btnBattle = null;

    private void Awake()
    {
        btnBattle.onClick.AddListener(OnClickGoBattle);
    }
    private void Start()
    {
        EnterScene();
    }

    public override void EnterScene()
    {
        Debug.Log("Lobby Setting");
    }

    public override void ExitScene()
    {

    }

    private void OnClickGoBattle()
    {
        SceneChanger.Instance.ChangeScene(SceneChanger.SceneType.Ingame, true);
    }
}

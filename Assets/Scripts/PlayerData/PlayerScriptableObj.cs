using UnityEngine;

[CreateAssetMenu(fileName = "PlayerScriptableObj", menuName = "Scriptable Objects/PlayerScriptableObj")]
public class PlayerScriptableObj : ScriptableObject
{
    [SerializeField]
    private PlayerData playerData = new PlayerData();

    public PlayerData PlayerData => playerData;
}

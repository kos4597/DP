using UnityEngine;

[CreateAssetMenu(fileName = "MonsterScriptableObj", menuName = "Scriptable Objects/MonsterScriptableObj")]
public class MonsterScriptableObj : ScriptableObject
{
    [SerializeField]
    private MonsterData monsterData = new MonsterData();

    public MonsterData MonsterData => monsterData;
}
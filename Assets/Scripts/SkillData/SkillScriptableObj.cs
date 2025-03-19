using UnityEngine;
[CreateAssetMenu(fileName = "SkillScriptableObj", menuName = "Scriptable Objects/SkillScriptableObj")]
public class SkillScriptableObj : ScriptableObject
{
    [SerializeField]
    private SkillData[] skillDataBase = null;

    public SkillData[] SkillDataBase => skillDataBase;
}
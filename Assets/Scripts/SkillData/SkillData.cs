using UnityEngine;
[System.Serializable]
public class SkillData
{
    [SerializeField]
    private string skillName = string.Empty; // 스킬명
    public string SkillName => skillName;

    [SerializeField]
    private float damage = 10f; // 데미지
    public float Damage => damage;

    [SerializeField]
    private float coolTime = 5f; // 쿨타임
    public float CoolTime => coolTime;

    [SerializeField]
    private string resourceName = string.Empty; // 로드할 프리팹명
    public string ResourceName => resourceName;

    [SerializeField]
    private float moveTime = 10f; // 스킬 날아가는 속도
    public float MoveTime => moveTime;
}

using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem particle = null;

    private Monster target = null;
    private SkillData skillData = null;

    private float elapsedTime = 0f;

    public void SetSkill(Monster target, SkillData skillData)
    {
        this.target = target;
        this.skillData = skillData;
    }

    public float GetSkillDamage()
    {
        return skillData.Damage;
    }

    private void Update()
    {
        if (target == null)
            return;

        elapsedTime += Time.deltaTime;
        float t = elapsedTime / skillData.MoveTime;

        Vector3 targetCenter = new Vector3(target.transform.position.x, target.transform.position.y + 1f, target.transform.position.z);

        transform.position = Vector3.Lerp(transform.position, targetCenter, t);

        if(Vector3.Distance(transform.position, targetCenter) < 1.5f)
        {
            target.HitMonster((int)skillData.Damage);
            Destroy(gameObject);
        }
    }
}

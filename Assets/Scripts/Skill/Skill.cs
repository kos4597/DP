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

        SetParticle();
    }

    private void SetParticle()
    {
        var triggerModule = particle.trigger;
        triggerModule.inside = ParticleSystemOverlapAction.Callback;
        triggerModule.enter = ParticleSystemOverlapAction.Callback;

        target.GetComponent<CapsuleCollider>().isTrigger = true;

        triggerModule.SetCollider(0, target.GetComponent<CapsuleCollider>());
    }

    private void OnParticleTrigger()
    {
        List<ParticleSystem.Particle> enterParticles = new List<ParticleSystem.Particle>();

        // Enter 이벤트 발생한 파티클 가져오기
        int numEnter = particle.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enterParticles);

        for (int i = 0; i < numEnter; i++)
        {
            Debug.Log("Collider Enter Particle");

            target.HitMonster((int)skillData.Damage);
        }
    }

    private void Update()
    {
        if (target == null)
            return;

        elapsedTime += Time.deltaTime;
        float t = elapsedTime / skillData.MoveTime;

        transform.position = Vector3.Lerp(transform.position, target.transform.position, t);
    }
}

using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEditor;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private Transform targetTr;

    [SerializeField]
    private Weapon weapon;

    private float angle = 30f;
    private float radius = 3f;

    private int attackStack = -1;
    private float lastAttackTime = 0f;
    private float attackDelayTime = 0.5f;

    private void Update()
    {
        if (Time.time - lastAttackTime > attackDelayTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Attack();
                lastAttackTime = Time.time;
            }
        }
    }

    private void Attack()
    {
        attackStack++;

        Debug.Log("stack : " + attackStack);
        animator.SetInteger("AttackStack", attackStack);
        Debug.Log(animator.GetInteger("AttackStack"));

        animator.SetTrigger("Attack");

        if (attackStack > 2)
        {
            attackStack = -1;
        }
    }

    public void OnEnableWeaponCollision()
    {
        weapon.GetComponent<SphereCollider>().enabled = true;
    }

    public void OnDisableWeaponCollision()
    {
        weapon.GetComponent<SphereCollider>().enabled = false;
    }
}

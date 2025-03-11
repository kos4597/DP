using System;
using System.Threading;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField]
    private Animator animator = null;
    [SerializeField]
    private CharacterController controller = null;

    public CharacterController Controller => controller;

    public Transform SpawnPoint { get; private set; }


    [SerializeField]
    private MonsterScriptableObj monsterSO;
    public MonsterScriptableObj MonsterSO => monsterSO;

    public Transform TrackingTargetTr { get; private set; }

    private MonsterStateMachine stateMachine;
    private Vector3 velocity;


    private void Awake()
    {
        Debug.Log("Monster Create");
    }
    private void Start()
    {
        stateMachine = new MonsterStateMachine(this);
        stateMachine.ChangeState(MonsterStateType.Idle);
    }

    private void Update()
    {
        ApplyGravity();
        stateMachine?.UpdateState();
    }

    public void Hit(int damage)
    {
        Debug.Log("Hit" + damage);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, MonsterSO.MonsterData.TrackingRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, MonsterSO.MonsterData.AttackRange);
    }



    public bool CheckPlayerInRange()
    {
        if (TrackingTargetTr == null) 
            return false;

        float distance = Vector3.Distance(transform.position, TrackingTargetTr.position);
        return distance <= MonsterSO.MonsterData.TrackingRange;
    }

    public bool CheckPlayerAttackRange()
    {
        if (TrackingTargetTr == null)
            return false;

        float distance = Vector3.Distance(transform.position, TrackingTargetTr.position);
        return distance <= MonsterSO.MonsterData.AttackRange;
    }

    public bool CheckRunAwayEnd()
    {
        float distance = Vector3.Distance(transform.position, SpawnPoint.position);
        return distance <= 2f;
    }

    public void SetTargetPlayer(Transform tr)
    {
        TrackingTargetTr = tr;
    }

    public void SetSpawnPoint(Transform tr)
    {
        SpawnPoint = tr;
    }

    private void ApplyGravity()
    {
        if (!controller.isGrounded)
        {
            velocity.y += MonsterSO.MonsterData.Gravity * Time.deltaTime * 5f;
            controller.Move(velocity * Time.deltaTime);
        }
        else
        {
            velocity.y = 0;
        }
    }
}

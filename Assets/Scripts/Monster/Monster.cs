using System;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    [SerializeField]
    private Animator animator = null;
    public NavMeshAgent Agent {  get; private set; }
    public Transform SpawnPoint { get; private set; }


    [SerializeField]
    private MonsterScriptableObj monsterSO;
    public MonsterScriptableObj MonsterSO => monsterSO;

    public Transform TrackingTargetTr { get; private set; }

    private MonsterStateMachine stateMachine;
    private Vector3 velocity;

    private double hp = 0;


    private void Awake()
    {
        Debug.Log("Monster Create");
        Agent = GetComponent<NavMeshAgent>();
        InitMonsterData();
    }
    private void Start()
    {
        stateMachine = new MonsterStateMachine(this);
        stateMachine.ChangeState(MonsterStateType.Idle);
    }

    private void Update()
    {
        stateMachine?.UpdateState();
    }

    private void InitMonsterData()
    {
        hp = monsterSO.MonsterData.HP;
        Agent.speed = MonsterSO.MonsterData.MoveSpeed;
    }

    public void HitMonster(int damage)
    {
        Debug.Log("Hit" + damage);
        hp -= damage;

        if(hp <= 0)
        {
            stateMachine.ChangeState(MonsterStateType.Dead);
        }
    }

    public void Dead()
    {
        hp = 0;
        IngameManager.Instance.monsterPool.Remove(this);
        Destroy(gameObject);
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
        Vector3 temp = new Vector3(SpawnPoint.position.x, transform.position.y, SpawnPoint.position.z);
        float distance = Vector3.Distance(transform.position, temp);
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
}

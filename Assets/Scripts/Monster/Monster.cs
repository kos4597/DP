using System;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField]
    private Animator animator = null;
    public Transform SpawnPoint { get; private set; }

    public float patrolRange = 10f; // ���� �ݰ�
    public float trackingRange = 10f; // �߰� �ݰ�
    public float speed = 10f; // �̵� �ӵ�
    public float rotationSpeed = 5f; // ȸ�� �ӵ�
    public float gravity = 9.81f; // �߷�

    private Transform targetPlayer = null;

    private MonsterStateMachine stateMachine;


    private void Awake()
    {
        Debug.Log("Monster Create");
    }
    private void Start()
    {
        stateMachine = new MonsterStateMachine(this);
        stateMachine.ChangeState(MonsterStateType.Patrol);
    }

    private void Update()
    {
        stateMachine?.UpdateState();
    }

    public void Hit(int damage)
    {
        Debug.Log("Hit" + damage);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, trackingRange);
    }

    public bool CheckPlayerInRange()
    {
        if (targetPlayer == null) 
            return false;

        float distance = Vector3.Distance(transform.position, targetPlayer.position);
        return distance <= trackingRange;
    }

    public bool CheckRunAwayEnd()
    {
        float distance = Vector3.Distance(transform.position, SpawnPoint.position);
        return distance <= 1f;
    }

    public void SetTargetPlayer(Transform tr)
    {
        targetPlayer = tr;
    }

    public void SetSpawnPoint(Transform tr)
    {
        SpawnPoint = tr;
    }

}

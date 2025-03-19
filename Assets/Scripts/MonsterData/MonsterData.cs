using UnityEngine;

[System.Serializable]
public class MonsterData
{
    [SerializeField]
    private float patrolRange = 10f; // 순찰 반경
    public float PatrolRange => patrolRange;

    [SerializeField]
    private float trackingRange = 10f; // 추격 반경
    public float TrackingRange => trackingRange;

    [SerializeField]
    private float attackRange = 2f; // 공격 반경

    public float AttackRange => attackRange;

    [SerializeField]
    private int attackSpeed = 1000; // 공격속도(ms)

    public int AttackSpeed => attackSpeed;

    [SerializeField]
    private float moveSpeed = 4f; // 이동 속도
    public float MoveSpeed => moveSpeed;

    [SerializeField]
    private float rotationSpeed = 5f; // 회전 속도
    public float RotationSpeed => rotationSpeed;

    [SerializeField]
    private double hp = 100;
    public double HP => hp;

    [SerializeField]
    private float gravity = -9.81f; // 중력
    public float Gravity => gravity;

    [SerializeField]
    private float raycastDistance = 0.5f; // 땅 체크용 Raycast 거리
    public float RaycastDistance => raycastDistance;
}

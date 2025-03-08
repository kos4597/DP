using UnityEngine;

/// <summary>
/// cs파일 만들 때 UTF8로 만들어지게 설정 하는게 좋음
/// UTF8이 아니면 인스펙터나 git 같은 곳에서 한글이 다 깨져서 주석이 하나도 안보임
/// 설정 방법은 기억 안나므로 찾아서 하시길....
/// </summary>
[SerializeField]
public class MonsterData
{
    [SerializeField]
    private float patrolRange = 10f; // 순찰 반경
    public float PatrolRange => patrolRange;

    [SerializeField]
    private float trackingRange = 5f; // 추격 반경
    public float TrackingRange => trackingRange;

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

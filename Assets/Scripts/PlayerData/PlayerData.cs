using UnityEngine;

[SerializeField]
public class PlayerData
{
    /// <summary>
    /// 프로퍼티 사용할 때
    /// WalkSpeed {get { return walkSpeed; } }형식을 사용하든 WalkSpeed => walkSpeed 형식을 사용하든 한가지만 사용할 것
    /// 다른 코드를 보면 { get; set; }으로 사용하는 곳도 있었음
    /// </summary>

    // 이런 정보들은 테이블로 빼거나 스크립터블오브젝트로 관리
    [SerializeField]
    private float walkSpeed = 2f; // 걷기 속도
    public float WalkSpeed => walkSpeed;
    [SerializeField]
    private float runSpeed = 8f; // 뛰기 속도
    public float RunSpeed => runSpeed;

    [SerializeField]
    private float turnSpeed = 10f; // 회전 속도
    public float TurnSpeed => turnSpeed;

    [SerializeField]
    private float jumpForce = 5f; // 점프 힘
    public float JumpForce => jumpForce;

    [SerializeField]
    private float verticalVelocity = 0f; // 수직 속도 저장
    public float VerticalVelocity => verticalVelocity;

    [SerializeField]
    private float gravity = -9.81f; // 중력
    public float Gravity => gravity;

    [SerializeField]
    private float raycastDistance = 0.5f; // 땅 체크용 Raycast 거리
    public float RaycastDistance => raycastDistance;

    [SerializeField]
    private float heightOffset = -0.1f; // 캐릭터와 지형 간 여유 높이
    public float HeightOffset => heightOffset;

    [SerializeField]
    private float attackDelayTime = 0.5f; // 공격 콤보 시간
    public float AttackDelayTime => attackDelayTime;

}

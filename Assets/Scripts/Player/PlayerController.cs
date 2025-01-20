using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera = null;
    [SerializeField]
    private Animator animator = null;
    [SerializeField]
    private CharacterController characterController = null;

    [SerializeField]
    private float walkSpeed = 4f;
    [SerializeField]
    private float runSpeed = 4f;
    public float turnSpeed = 10f;  // 회전 속도

    private int attackStack = -1;

    private Vector3 movement = Vector3.zero;

    private LayerMask groundLayer;
    private float verticalVelocity = 0f; // 수직 속도 저장
    public float gravity = -9.81f; // 중력
    public float raycastDistance = 10f; // Raycast 거리
    public float heightOffset = 0.5f; // 캐릭터와 지형 간 여유 높이


    void Update()
    {
        MoveCharacter();
    }

    void MoveCharacter()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        float speed = Mathf.Clamp01(new Vector2(horizontal, vertical).magnitude); // 크기(0~1)
        animator.SetFloat("MoveSpeed", speed);
        animator.SetBool("Idle", speed <= 0);

        // 캐릭터 이동 처리
        movement = new Vector3(horizontal, 0, vertical).normalized;

        //transform.Translate(movement, Space.World);

        // 캐릭터 회전 처리 (방향 전환)
        if (movement.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSpeed, 0.1f);
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }

        // **Raycast로 지면 감지 및 높이 조정**
        if (IsGrounded(out float groundHeight))
        {
            // 캐릭터를 지형 위에 붙임
            verticalVelocity = 0f; // 땅 위에 있을 때 수직 속도 초기화
            Vector3 position = transform.position;
            position.y = groundHeight + heightOffset;
            transform.position = position;
        }
        else
        {
            // 공중에 있으면 중력을 적용
            verticalVelocity += gravity * Time.deltaTime;
        }

        // **캐릭터 이동 처리**
        Vector3 verticalMovement = new Vector3(0, verticalVelocity, 0);
        characterController.Move((movement * walkSpeed + verticalMovement) * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            characterController.Move(movement * runSpeed + verticalMovement * Time.deltaTime);
        }
    }

    bool IsGrounded(out float groundHeight)
    {
        RaycastHit hit;
        Vector3 rayOrigin = transform.position + Vector3.up * raycastDistance;

        // Raycast로 지면 감지
        if (Physics.Raycast(rayOrigin, Vector3.down, out hit, raycastDistance, groundLayer))
        {
            groundHeight = hit.point.y; // 충돌한 지형의 높이 반환
            return true;
        }

        groundHeight = 0f;
        return false;
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");
        attackStack++;
        animator.SetInteger("AttackStack", attackStack);

        if (attackStack >= 2)
        {
            attackStack = -1;
        }
    }

    public void SetPlayerCamera()
    {
        mainCamera = Camera.main;
    }
}

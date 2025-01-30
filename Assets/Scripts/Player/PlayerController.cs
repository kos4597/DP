using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera = null;
    [SerializeField]
    private Animator animator = null;
    [SerializeField]
    private CharacterController characterController = null;


    // 이런 정보들은 테이블로 빼거나 스크립터블오브젝트로 관리

    private float walkSpeed = 2f; // 걷기 속도
    private float runSpeed = 4f; // 달리기 속도
    private float turnSpeed = 10f;  // 회전 속도

    private float jumpForce = 5f; // 점프 힘

    private float verticalVelocity = 0f; // 수직 속도 저장
    private float gravity = -9.81f; // 중력
    private float raycastDistance = 0.5f; // Raycast 거리
    private float heightOffset = 0.1f; // 캐릭터와 지형 간 여유 높이

    private LayerMask groundLayer;


    private void Awake()
    {
        groundLayer = LayerMask.GetMask("Ground");
    }


    void Update()
    {
        MoveCharacter();
    }

    // 전반적으로 예외처리 코드가 부족한 듯

    void MoveCharacter()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1") ||
           animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2") ||
           animator.GetCurrentAnimatorStateInfo(0).IsName("Attack3"))
        {
            return;
        }

        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        float inputMagnitude = new Vector2(horizontal, vertical).magnitude;
        float calcSpeed = Mathf.Clamp01(inputMagnitude);
        float speed = Mathf.Lerp(animator.GetFloat("MoveSpeed"), calcSpeed, Time.deltaTime * 10f);

        if (inputMagnitude < 0.01f)
        {
            speed = 0;
        }

        // 이런 키값들은 StringDefine class를 만들어서 readonly로 Hash를 미리 얻은 후 String 대신 Hash로 호출할 것
        // animator.SetBool(RunHash(int Hash 값), false);

        animator.SetBool("Run", false);
        animator.SetFloat("MoveSpeed", speed);
        animator.SetBool("Idle", speed <= 0);

        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;
        cameraForward.Normalize();
        cameraRight.Normalize();

        // 캐릭터 이동 처리
        Vector3 movement = (cameraForward * vertical + cameraRight * horizontal).normalized;

        // 캐릭터 회전 처리 (방향 전환)
        if (movement.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSpeed, 0.1f);
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }

        if (IsGrounded(out float groundHeight))
        {
            if (verticalVelocity < 0)
                verticalVelocity = 0f;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = jumpForce;
                animator.SetTrigger("Jump");
            }

            Vector3 position = transform.position;
            position.y = groundHeight + heightOffset;
            transform.position = position;
        }
        else
        {
            // 공중에 있으면 중력을 적용
            verticalVelocity += gravity * Time.deltaTime;
        }

        Vector3 verticalMovement = new Vector3(0, verticalVelocity, 0);
        characterController.Move((movement * walkSpeed + verticalMovement) * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool("Run", true);
            characterController.Move((movement * runSpeed + verticalMovement) * Time.deltaTime);
        }
    }

    private bool IsGrounded(out float groundHeight)
    {
        RaycastHit hit;
        Vector3 rayOrigin = transform.position + Vector3.up * 0.2f;
        Debug.DrawRay(rayOrigin, Vector3.down * raycastDistance, Color.red, 1f);

        if (Physics.Raycast(rayOrigin, Vector3.down, out hit, raycastDistance, groundLayer))
        {
            groundHeight = hit.point.y;
            return true;
        }

        groundHeight = 0f;
        return false;
    }

    public void SetPlayerCamera()
    {
        mainCamera = Camera.main;
    }
}

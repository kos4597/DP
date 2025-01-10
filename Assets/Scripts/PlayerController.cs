using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera = null;
    [SerializeField]
    private Animator animator = null;
    [SerializeField]
    private CharacterController controller = null;

    [SerializeField]
    private float walkSpeed = 5f;
    [SerializeField]
    private float runSpeed = 10f;

    private int attackStack = -1;

    public float moveSpeed = 5f; // 이동 속도
    public float rotationSpeed = 5f; // 회전 속도

    public float minMouseDistance = 2f;

    void Update()
    {
        RotateToMouse();
        MoveCharacter();
    }

    // 캐릭터를 마우스 위치를 따라 회전
    void RotateToMouse()
    {
        // 마우스 위치를 가져옴
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 targetPosition = hit.point;

            // 캐릭터와 마우스 간의 거리 계산
            float distance = Vector3.Distance(transform.position, targetPosition);

            // 최소 거리 제한
            if (distance > minMouseDistance)
            {
                targetPosition.y = transform.position.y; // 높이 고정
                Vector3 direction = (targetPosition - transform.position).normalized;

                // 목표 회전 계산
                Quaternion targetRotation = Quaternion.LookRotation(direction);

                // 현재 회전과 목표 회전을 오일러 각도로 변환
                float currentYRotation = NormalizeAngle(transform.eulerAngles.y);
                float targetYRotation = NormalizeAngle(targetRotation.eulerAngles.y);

                // 회전 각도 차이 계산
                float angleDifference = Mathf.DeltaAngle(currentYRotation, targetYRotation);

                // 제한된 목표 회전을 다시 Quaternion으로 변환
                Quaternion clampedRotation = Quaternion.Euler(0, angleDifference, 0);

                // 부드러운 회전 적용
                transform.rotation = Quaternion.Lerp(transform.rotation, clampedRotation, Time.deltaTime * rotationSpeed);
            }
        }
    }

    // WASD 키 입력에 따라 이동
    void MoveCharacter()
    {
        float horizontal = Input.GetAxis("Horizontal"); // A, D 또는 좌우 화살표
        float vertical = Input.GetAxis("Vertical"); // W, S 또는 위아래 화살표

        Vector3 moveDirection = new Vector3(horizontal, 0, vertical).normalized;

        if (moveDirection.magnitude > 0.1f)
        {
            animator.ResetTrigger("Idle");
            animator.SetTrigger("Walk");

            // 카메라의 정면 방향 기준으로 이동 방향 설정
            Vector3 move = transform.forward * moveDirection.z + transform.right * moveDirection.x;
            transform.position += move * moveSpeed * Time.deltaTime;
        }
        else
        {
            animator.ResetTrigger("Walk");
            animator.SetTrigger("Idle");
        }
    }

    float NormalizeAngle(float angle)
    {
        while (angle < 0) angle += 360f;
        while (angle >= 360f) angle -= 360f;
        return angle;
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
}

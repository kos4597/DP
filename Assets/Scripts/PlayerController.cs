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

    public float moveSpeed = 5f; // �̵� �ӵ�
    public float rotationSpeed = 5f; // ȸ�� �ӵ�

    public float minMouseDistance = 2f;

    void Update()
    {
        RotateToMouse();
        MoveCharacter();
    }

    // ĳ���͸� ���콺 ��ġ�� ���� ȸ��
    void RotateToMouse()
    {
        // ���콺 ��ġ�� ������
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 targetPosition = hit.point;

            // ĳ���Ϳ� ���콺 ���� �Ÿ� ���
            float distance = Vector3.Distance(transform.position, targetPosition);

            // �ּ� �Ÿ� ����
            if (distance > minMouseDistance)
            {
                targetPosition.y = transform.position.y; // ���� ����
                Vector3 direction = (targetPosition - transform.position).normalized;

                // ��ǥ ȸ�� ���
                Quaternion targetRotation = Quaternion.LookRotation(direction);

                // ���� ȸ���� ��ǥ ȸ���� ���Ϸ� ������ ��ȯ
                float currentYRotation = NormalizeAngle(transform.eulerAngles.y);
                float targetYRotation = NormalizeAngle(targetRotation.eulerAngles.y);

                // ȸ�� ���� ���� ���
                float angleDifference = Mathf.DeltaAngle(currentYRotation, targetYRotation);

                // ���ѵ� ��ǥ ȸ���� �ٽ� Quaternion���� ��ȯ
                Quaternion clampedRotation = Quaternion.Euler(0, angleDifference, 0);

                // �ε巯�� ȸ�� ����
                transform.rotation = Quaternion.Lerp(transform.rotation, clampedRotation, Time.deltaTime * rotationSpeed);
            }
        }
    }

    // WASD Ű �Է¿� ���� �̵�
    void MoveCharacter()
    {
        float horizontal = Input.GetAxis("Horizontal"); // A, D �Ǵ� �¿� ȭ��ǥ
        float vertical = Input.GetAxis("Vertical"); // W, S �Ǵ� ���Ʒ� ȭ��ǥ

        Vector3 moveDirection = new Vector3(horizontal, 0, vertical).normalized;

        if (moveDirection.magnitude > 0.1f)
        {
            animator.ResetTrigger("Idle");
            animator.SetTrigger("Walk");

            // ī�޶��� ���� ���� �������� �̵� ���� ����
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

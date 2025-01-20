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
    public float turnSpeed = 10f;  // ȸ�� �ӵ�

    private int attackStack = -1;

    private Vector3 movement = Vector3.zero;

    private LayerMask groundLayer;
    private float verticalVelocity = 0f; // ���� �ӵ� ����
    public float gravity = -9.81f; // �߷�
    public float raycastDistance = 10f; // Raycast �Ÿ�
    public float heightOffset = 0.5f; // ĳ���Ϳ� ���� �� ���� ����


    void Update()
    {
        MoveCharacter();
    }

    void MoveCharacter()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        float speed = Mathf.Clamp01(new Vector2(horizontal, vertical).magnitude); // ũ��(0~1)
        animator.SetFloat("MoveSpeed", speed);
        animator.SetBool("Idle", speed <= 0);

        // ĳ���� �̵� ó��
        movement = new Vector3(horizontal, 0, vertical).normalized;

        //transform.Translate(movement, Space.World);

        // ĳ���� ȸ�� ó�� (���� ��ȯ)
        if (movement.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSpeed, 0.1f);
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }

        // **Raycast�� ���� ���� �� ���� ����**
        if (IsGrounded(out float groundHeight))
        {
            // ĳ���͸� ���� ���� ����
            verticalVelocity = 0f; // �� ���� ���� �� ���� �ӵ� �ʱ�ȭ
            Vector3 position = transform.position;
            position.y = groundHeight + heightOffset;
            transform.position = position;
        }
        else
        {
            // ���߿� ������ �߷��� ����
            verticalVelocity += gravity * Time.deltaTime;
        }

        // **ĳ���� �̵� ó��**
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

        // Raycast�� ���� ����
        if (Physics.Raycast(rayOrigin, Vector3.down, out hit, raycastDistance, groundLayer))
        {
            groundHeight = hit.point.y; // �浹�� ������ ���� ��ȯ
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

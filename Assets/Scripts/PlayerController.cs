using UnityEngine;

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

    private int attackStack = -1;

    private Vector3 movement = Vector3.zero;

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
        movement = new Vector3(horizontal, 0, vertical).normalized * Time.deltaTime;
        characterController.Move(movement * walkSpeed * Time.deltaTime);

        //transform.Translate(movement, Space.World);

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            characterController.Move(movement * Time.deltaTime * runSpeed);
        }

        // 캐릭터 회전 처리 (방향 전환)
        if (movement.magnitude > 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
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

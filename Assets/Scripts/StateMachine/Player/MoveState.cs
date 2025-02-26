using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class MoveState : BaseState
{
    public MoveState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }
    public override void OnStateEnter()
    {

    }

    public override void OnStateUpdate()
    {
        if (player.CheckAttack())
        {
            playerStateMachine.ChangeState(PlayerStateType.Attack);
        }

        else if (player.CheckMoveInput() == false)
        {
            playerStateMachine.ChangeState(PlayerStateType.Idle);
        }

        else
        {
            MovePlayer();
        }

    }

    public override void OnStateExit()
    {
        Utility.SetAnimaion(player.GetAnimator(), StringDefine.RUN_ANI_HASH, false);
        Debug.Log($"Exit Move");
        Utility.SetAnimaion(player.GetAnimator(), StringDefine.MOVESPEED_ANI_HASH, 0);
    }

    private void MovePlayer()
    {
        Transform playerTr = player.transform;
        CharacterController controller = player.GetComponent<CharacterController>();
        Animator animator = player.GetAnimator();

        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        float inputMagnitude = new Vector2(horizontal, vertical).magnitude;
        float calcSpeed = Mathf.Clamp01(inputMagnitude);
        float speed = Mathf.Lerp(Utility.GetAnimFloatParam(animator, StringDefine.MOVESPEED_ANI_HASH), calcSpeed, Time.deltaTime * 10f);

        if (inputMagnitude < 0.1f)
        {
            speed = 0;
        }

        Utility.SetAnimaion(animator, StringDefine.RUN_ANI_HASH, false);
        Utility.SetAnimaion(animator, StringDefine.MOVESPEED_ANI_HASH, speed);

        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;
        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 movement = (cameraForward * vertical + cameraRight * horizontal).normalized;

        // 캐릭터 회전 처리 (방향 전환)
        if (movement.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg;
            float turnSpeed = player.PlayerSO.PlayerData.TurnSpeed;
            float angle = Mathf.SmoothDampAngle(playerTr.eulerAngles.y, targetAngle, ref turnSpeed, 0.1f);
            playerTr.rotation = Quaternion.Euler(0, angle, 0);
        }

        float verticalVel = player.PlayerSO.PlayerData.VerticalVelocity;

        if (IsGrounded(out float groundHeight))
        {
            if (verticalVel < 0)
                verticalVel = 0f;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVel = player.PlayerSO.PlayerData.JumpForce;
                Utility.SetAnimaion(animator, StringDefine.JUMP_ANI_HASH);
            }

            Vector3 position = playerTr.position;
            position.y = groundHeight + player.PlayerSO.PlayerData.HeightOffset;
            playerTr.position = position;
        }
        else
        {
            // 공중에 있으면 중력을 적용
            verticalVel += player.PlayerSO.PlayerData.Gravity * Time.deltaTime * 5f;
        }

        Vector3 verticalMovement = new Vector3(0, verticalVel, 0);
        controller.Move((movement * player.PlayerSO.PlayerData.WalkSpeed + verticalMovement) * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Utility.SetAnimaion(animator, StringDefine.RUN_ANI_HASH, true);
            controller.Move((movement * player.PlayerSO.PlayerData.WalkSpeed + verticalMovement) * Time.deltaTime);
        }
    }

    private bool IsGrounded(out float groundHeight)
    {
        Transform playerTr = player.transform;

        RaycastHit hit;
        Vector3 rayOrigin = playerTr.position + Vector3.up * 0.2f;
        Debug.DrawRay(rayOrigin, Vector3.down * player.PlayerSO.PlayerData.RaycastDistance, Color.red, 1f);
        LayerMask groundLayer = LayerMask.GetMask("Ground"); // 땅 레이어

        if (Physics.Raycast(rayOrigin, Vector3.down, out hit, player.PlayerSO.PlayerData.RaycastDistance, groundLayer))
        {
            groundHeight = hit.point.y;
            return true;
        }

        groundHeight = 0f;
        return false;
    }
}

using Unity.Android.Gradle.Manifest;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

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
            Move();
        }

    }

    public override void OnStateExit()
    {
        player.GetAnimator().SafeSetAnimaion(StringDefine.RUN_ANI_HASH, false);
        Debug.Log($"Exit Move");
        player.GetAnimator().SafeSetAnimaion(StringDefine.MOVESPEED_ANI_HASH, 0f);
    }

    private void Move()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        Vector2 inputVector = new Vector2(horizontal, vertical);
        Vector3 dirVec = Camera.main.transform.right * inputVector.x + Camera.main.transform.forward * inputVector.y;
        player.Agent.SetDestination(player.transform.position + dirVec);

        Animator animator = player.GetAnimator();

        float calcSpeed = Mathf.Clamp01(inputVector.magnitude);
        float speed = Mathf.Lerp(animator.SafeGetAnimFloatParam(StringDefine.MOVESPEED_ANI_HASH), calcSpeed, Time.deltaTime * 10f);

        if (inputVector.magnitude < 0.1f)
            speed = 0;

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
            float angle = Mathf.SmoothDampAngle(player.transform.eulerAngles.y, targetAngle, ref turnSpeed, 0.1f);
            player.transform.rotation = Quaternion.Euler(0, angle, 0);
        }

        animator.SafeSetAnimaion(StringDefine.RUN_ANI_HASH, false);
        animator.SafeSetAnimaion(StringDefine.MOVESPEED_ANI_HASH, speed);
        player.Agent.speed = player.PlayerSO.PlayerData.WalkSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            animator.SafeSetAnimaion(StringDefine.RUN_ANI_HASH, true);
            player.Agent.speed = player.PlayerSO.PlayerData.RunSpeed;
        }
    }

    //private void MovePlayer()
    //{
    //    Transform playerTr = player.transform;

    //    float vertical = Input.GetAxis("Vertical");
    //    float horizontal = Input.GetAxis("Horizontal");
    //    Animator animator = player.GetAnimator();

    //    float inputMagnitude = new Vector2(horizontal, vertical).magnitude;
    //    float speed = Mathf.Lerp(animator.SafeGetAnimFloatParam(StringDefine.MOVESPEED_ANI_HASH), inputMagnitude, Time.deltaTime * 10f);

    //    if (inputMagnitude < 0.1f)
    //    {
    //        speed = 0;
    //    }

    //    animator.SafeSetAnimaion(StringDefine.RUN_ANI_HASH, false);
    //    animator.SafeSetAnimaion(StringDefine.MOVESPEED_ANI_HASH, speed);

    //    Vector3 cameraForward = Camera.main.transform.forward;
    //    Vector3 cameraRight = Camera.main.transform.right;

    //    cameraForward.y = 0f;
    //    cameraRight.y = 0f;
    //    cameraForward.Normalize();
    //    cameraRight.Normalize();

    //    Vector3 movement = (cameraForward * vertical + cameraRight * horizontal).normalized;

    //    // 캐릭터 회전 처리 (방향 전환)
    //    if (movement.magnitude >= 0.1f)
    //    {
    //        float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg;
    //        float turnSpeed = player.PlayerSO.PlayerData.TurnSpeed;
    //        float angle = Mathf.SmoothDampAngle(playerTr.eulerAngles.y, targetAngle, ref turnSpeed, 0.1f);
    //        playerTr.rotation = Quaternion.Euler(0, angle, 0);
    //    }

    //    float verticalVel = player.PlayerSO.PlayerData.VerticalVelocity;

    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        verticalVel = player.PlayerSO.PlayerData.JumpForce;
    //        animator.SafeSetAnimaion(StringDefine.JUMP_ANI_HASH);
    //    }

    //    Vector3 verticalMovement = new Vector3(0, verticalVel, 0);
    //    player.Controller.Move((movement * player.PlayerSO.PlayerData.WalkSpeed + verticalMovement) * Time.deltaTime);

    //    if (Input.GetKey(KeyCode.LeftShift))
    //    {
    //        animator.SafeSetAnimaion(StringDefine.RUN_ANI_HASH, true);
    //        player.Controller.Move((movement * player.PlayerSO.PlayerData.WalkSpeed + verticalMovement) * Time.deltaTime);
    //    }
    //}
}

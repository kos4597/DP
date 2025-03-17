using System.Threading;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Animator animator = null;
    [SerializeField]
    private Weapon weapon = null;
    [SerializeField]
    private CharacterController controller = null;
    public CharacterController Controller => controller;

    [SerializeField]
    private PlayerScriptableObj playerSO;
    public PlayerScriptableObj PlayerSO => playerSO;

    private PlayerStateMachine stateMachine;

    private Vector3 velocity;

    public bool AttackAniEndFlag { get; private set; }

    private void Awake()
    {
        Debug.Log("Player Create");
    }

    private void Start()
    {
        stateMachine = new PlayerStateMachine(this);
        stateMachine.ChangeState(PlayerStateType.Idle);
    }

    private void Update()
    {
        ApplyGravity();
        stateMachine?.UpdateState();
    }

    public bool CheckMoveInput()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        float inputMagnitude = new Vector2(horizontal, vertical).magnitude;

        return inputMagnitude > 0f;
    }

    public bool CheckAttack()
    {
        return Input.GetMouseButtonDown(0);
    }

    public bool CheckSkill()
    {
        return Input.anyKeyDown && IngameManager.Instance.skillSet.ContainsKey(Input.inputString);
    }

    public void OnEnableWeaponCollision()
    {
        weapon.GetComponent<SphereCollider>().enabled = true;
    }

    public void OnDisableWeaponCollision()
    {
        weapon.GetComponent<SphereCollider>().enabled = false;
    }

    public Animator GetAnimator()
    {
        if (animator == null)
        {
            animator = this.GetComponent<Animator>();
        }

        return animator;
    }

    //Animation Callback Method
    public void CheckAttackEnd()
    {
        AttackAniEndFlag = true;
    }

    public void AttackEnd(bool isEnd)
    {
        AttackAniEndFlag = isEnd;
    }
    private void ApplyGravity()
    {
        if (!controller.isGrounded)
        {
            velocity.y += playerSO.PlayerData.Gravity * Time.deltaTime * 5f;
            controller.Move(velocity * Time.deltaTime);
        }
        else
        {
            velocity.y = 0;
        }
    }

    public void Hit(int damage)
    {
        Debug.Log("HitPlayer" + damage);
    }
}

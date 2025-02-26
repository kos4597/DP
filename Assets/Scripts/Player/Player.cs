using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Animator animator = null;
    [SerializeField]
    private Weapon weapon = null;

    [SerializeField]
    public PlayerScriptableObj PlayerSO;

    private PlayerStateMachine stateMachine;

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
        stateMachine?.UpdateState();
    }

    public bool CheckMoveInput()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        float inputMagnitude = new Vector2(horizontal, vertical).magnitude;

        Debug.Log($"MoveInput: {inputMagnitude}");
        return inputMagnitude > 0f;
    }

    public bool CheckAttack()
    {
        return Input.GetMouseButtonDown(0);
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
}

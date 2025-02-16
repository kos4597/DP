using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Animator animator = null;
    [SerializeField]
    private Weapon weapon = null;
    [SerializeField]
    private CharacterController characterController = null;

    [SerializeField]
    public PlayerScriptableObj PlayerSO;

    private StateType curState;
    private PlayerStateMachine fsm;

    public bool AttackAniEndFlag { get; private set; }


    private void Awake()
    {
        Debug.Log("Player Create");
    }

    private void Start()
    {
        fsm = new PlayerStateMachine(this);
        fsm.ChangeState(StateType.Idle);
    }

    private void Update()
    {
        fsm?.UpdateState();
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


    //Animation Callback Method
    public void CheckAttackEnd()
    {
        AttackAniEndFlag = true;
    }

    public void AttackEnd(bool isEnd)
    {
        AttackAniEndFlag = isEnd;
    }

    /// <summary>
    /// Trigger Param
    /// </summary>
    /// <param name="hash"></param>
    public void SetAnimaion(int hash)
    {
        if (animator == null)
            return;

        animator.SetTrigger(hash);
    }
    /// <summary>
    /// Bool param
    /// </summary>
    /// <param name="hash"></param>
    /// <param name="isOn"></param>
    public void SetAnimaion(int hash, bool isOn)
    {
        if (animator == null)
            return;

        animator.SetBool(hash, isOn);
    }
    /// <summary>
    /// Int Param
    /// </summary>
    /// <param name="hash"></param>
    /// <param name="count"></param>
    public void SetAnimaion(int hash, int count)
    {
        if (animator == null)
            return;

        animator.SetInteger(hash, count);
    }
    /// <summary>
    /// Float Param
    /// </summary>
    /// <param name="hash"></param>
    /// <param name="count"></param>
    public void SetAnimaion(int hash, float count)
    {
        if (animator == null)
            return;

        animator.SetFloat(hash, count);
    }


    public float GetAnimFloatParam(int hash)
    {
        return animator.GetFloat(hash);
    }
}

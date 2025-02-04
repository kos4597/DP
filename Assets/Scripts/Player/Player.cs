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

    private BaseState.StateType curState;
    private PlayerStateMachine fsm;

    private bool attackAniEndFlag = false;


    private void Awake()
    {
        Debug.Log("Player Create");
    }

    private void Start()
    {
        curState = BaseState.StateType.Idle;
        fsm = new PlayerStateMachine(new IdleState(this));
    }

    private void Update()
    {
        fsm?.UpdateState();
        switch (curState)
        {
            case BaseState.StateType.Idle:
                {
                    if (CheckAttack())
                    {
                        ChangeState(BaseState.StateType.Attack);
                    }
                    else if (CheckMoveInput())
                    {
                        ChangeState(BaseState.StateType.Move);
                    }
                }
                break;
            case BaseState.StateType.Move:
                {
                    if (CheckAttack())
                    {
                        ChangeState(BaseState.StateType.Attack);
                    }

                    else if (CheckMoveInput() == false)
                    {
                        ChangeState(BaseState.StateType.Idle);
                    }
                }
                break;
            case BaseState.StateType.Attack:
                {
                    if (attackAniEndFlag)
                    {
                        ChangeState(BaseState.StateType.Idle);
                    }
                }
                break;
        }
    }

    private void ChangeState(BaseState.StateType nextState)
    {
        curState = nextState;

        Debug.Log($"Change State {curState}");
        switch(curState)
        {
            case BaseState.StateType.Idle:
                {
                    attackAniEndFlag = false;
                    fsm.ChangeState(new IdleState(this));
                }
                break;
            case BaseState.StateType.Move:
                {
                    fsm.ChangeState(new MoveState(this));
                }
                break;
            case BaseState.StateType.Attack:
                {
                    fsm.ChangeState(new AttackState(this));
                }
                break;
        }
    }

    private bool CheckMoveInput()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        float inputMagnitude = new Vector2(horizontal, vertical).magnitude;

        Debug.Log($"MoveInput: {inputMagnitude}");
        return inputMagnitude > 0f && attackAniEndFlag == false;
    }

    private bool CheckAttack()
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

    public void CheckAttackEnd()
    {
        attackAniEndFlag = true;
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

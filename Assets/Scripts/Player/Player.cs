using UnityEngine;


//using System.Collections.Generic;
//public enum StateType 
//{
//    None,
//    Idle,
//    Attack,

//    Max,
//}

//public class PlayerStateMachine : IStateMachine
//{
//    public StateType CurrentStateType { get; private set; }

//    private Dictionary<StateType, IState> states = null;

//    public PlayerStateMachine(Player player)
//    {
//        states = new Dictionary<StateType, IState>(StateType.Max);
//        states.Add(StateType.Idle, new IdleState(player, this));
//        states.Add(StateType.Attack, new AttackState(player, this));
//    }

//    public void OnStateChange(StateType stateType)
//    {
//        if (CurrentStateType == stateType)
//            return;

//        if (CurrentStateType != stateType.None)
//            states[CurrentStateType].OnExit();

//        CurrentStateType = state;

//        states[CurrentStateType].OnEnter();
//    }

//    public void OnUpdate()
//    {
//        states[CurrentStateType].OnUpdate();
//    }
//}

//public class IdleState : IState // 네이밍이나 네임스페이스 같은걸로 Player Idle, Monster Idle 구분을 주면 좋음
//{
//    private Player player = null;
//    private PlayerStateMachine stateMachine = null;

//    public IdleState(Player player, PlayerStateMachine stateMachine)
//    {
//        this.player = player;
//        this.stateMachine = stateMachine;
//    }

//    public void OnEnter()
//    {
//    }

//    public void OnExit()
//    {
//    }

//    public void OnUpdate()
//    {
// 기본적인 행동과 관련된 상태는 SubState를 만드는것도 좋은 방법이 될 것
// 예) MoveState -> LocomotionState
// LocomotionState는 내부적으로 이동, 점프, 슬라이딩 등의 SubState들을 가지고 있음
//        if (공격키) // player.AttackInput()
//        {
//            stateMachine.OnStateChange(StateType.Attack);
//        }
//        else if (이동키) // player.MoveInput()
//        {
//            stateMachine.OnStateChange(StateType.Move);
//        }
//    }
//}


//public class Player : MonoBehaviour
//{
//    private PlayerStateMachine stateMachine;

//    private void Awake()
//    {
//        stateMachine = new PlayerStateMachine(this);
//        stateMachine.OnStateChange(StateType.Idle);
//    }

//    private void Update()
//    {
//        stateMachine.OnUpdate();
//    }
//}


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
        switch (curState)
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

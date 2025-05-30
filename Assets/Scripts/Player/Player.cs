using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Animator animator = null;
    [SerializeField]
    private Weapon weapon = null;
    [SerializeField]
    private Transform skillTr = null;
    public Transform SkillTr => skillTr;

    public NavMeshAgent Agent { get; private set; }

    [SerializeField]
    private PlayerScriptableObj playerSO;
    public PlayerScriptableObj PlayerSO => playerSO;

    private PlayerStateMachine stateMachine;

    public SkillData selectedSkill { get; private set; }

    private Vector3 velocity;

    public bool AttackAniEndFlag { get; private set; }

    private void Awake()
    {
        Debug.Log("Player Create");
        Agent = GetComponent<NavMeshAgent>();
        Agent.speed = PlayerSO.PlayerData.WalkSpeed;
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

        return inputMagnitude > 0f;
    }

    public bool CheckAttack()
    {
        return Input.GetMouseButtonDown(0);
    }

    public bool CheckSkill()
    {
        if (Input.anyKeyDown && IngameManager.Instance.skillSet.ContainsKey(Input.inputString))
        {
            SelectSkill();
            return true;
        }

        return false;
    }

    public void SelectSkill()
    {
        IngameManager.Instance.skillSet.TryGetValue(Input.inputString, out var skill);
        if(skill != null)
        {
            selectedSkill = skill;
        }
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

    public void Hit(int damage)
    {
        Debug.Log("HitPlayer" + damage);
    }
}

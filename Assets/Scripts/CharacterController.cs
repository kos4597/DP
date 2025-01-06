using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    private Animator animator = null;

    private int attackStack = -1;

    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Attack();
        }

        if(Input.GetMouseButtonUp(0))
        {
            animator.ResetTrigger("Attack");
            animator.SetInteger("AttackStack", attackStack);
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

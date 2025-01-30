using UnityEngine;

public class AttackAnimReset : StateMachineBehaviour
{
    [SerializeField]
    private string triggerName = null;
    [SerializeField]
    private string stackTriggerName = null;

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger(triggerName);
        animator.SetInteger(stackTriggerName, -1);
    }
}

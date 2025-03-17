using UnityEngine;

public class AttackAnimReset : StateMachineBehaviour
{
    [SerializeField]
    private string triggerName = null;
    [SerializeField]
    private string stackTriggerName = null;

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(string.IsNullOrEmpty(triggerName) == false)
            animator.ResetTrigger(triggerName);

        if(string.IsNullOrEmpty(stackTriggerName) == false)
            animator.SetInteger(stackTriggerName, -1);
    }
}

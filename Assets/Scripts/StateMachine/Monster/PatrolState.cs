using TMPro;
using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class PatrolState : BaseState
{
    public PatrolState(Monster monster, MonsterStateMachine stateMachine) : base(monster, stateMachine) { }

    private Vector3 targetPosition;
    private CharacterController characterController;
    private Vector3 velocity;

    public override void OnStateEnter()
    {
        characterController = monster.GetComponent<CharacterController>();
        SetRandomDestination();
    }

    public override void OnStateUpdate()
    {
        if(monster.CheckPlayerInRange())
        {
            monsterStateMachine?.ChangeState(MonsterStateType.Tracking);
        }
        else
        {
            Patrol();
            ApplyGravity();
        }
    }

    public override void OnStateExit()
    {

    }

    private void Patrol()
    {
        Vector3 direction = (targetPosition - monster.transform.position).normalized;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            monster.transform.rotation = Quaternion.Slerp(monster.transform.rotation, lookRotation, monster.rotationSpeed * Time.deltaTime);
        }

        characterController.Move(direction * monster.speed * Time.deltaTime);

        if (Vector3.Distance(monster.transform.position, targetPosition) < 0.5f)
        {
            SetRandomDestination();
        }
    }

    private void ApplyGravity()
    {
        if (!characterController.isGrounded)
        {
            velocity.y -= monster.gravity * Time.deltaTime;
            characterController.Move(velocity * Time.deltaTime);
        }
        else
        {
            velocity.y = 0;
        }
    }

    private void SetRandomDestination()
    {
        Vector2 randomCircle = Random.insideUnitCircle * monster.patrolRange;
        targetPosition = monster.transform.position + new Vector3(randomCircle.x, 0, randomCircle.y);
    }

}

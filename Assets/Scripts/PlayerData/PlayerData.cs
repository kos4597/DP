using UnityEngine;
[SerializeField]
public class PlayerData
{
    // �̷� �������� ���̺�� ���ų� ��ũ���ͺ������Ʈ�� ����
    [SerializeField] 
    private float walkSpeed = 2f; // �ȱ� �ӵ�
    public float WalkSpeed => walkSpeed;
    [SerializeField] 
    private float runSpeed = 4f; // �ٱ� �ӵ�
    public float RunSpeed => runSpeed;

    [SerializeField] 
    private float turnSpeed = 10f; // ȸ�� �ӵ�
    public float TurnSpeed => turnSpeed;

    [SerializeField] 
    private float jumpForce = 5f; // ���� ��
    public float JumpForce => jumpForce;

    [SerializeField] 
    private float verticalVelocity = 0f; // ���� �ӵ� ����
    public float VerticalVelocity => verticalVelocity;

    [SerializeField] 
    private float gravity = -9.81f; // �߷�
    public float Gravity => gravity;

    [SerializeField] 
    private float raycastDistance = 0.5f; // �� üũ�� Raycast �Ÿ�
    public float RaycastDistance => raycastDistance;

    [SerializeField] 
    private float heightOffset = -0.1f; // ĳ���Ϳ� ���� �� ���� ����
    public float HeightOffset => heightOffset;

    [SerializeField]
    private float attackDelayTime = 0.5f; // ���� �޺� �ð�
    public float AttackDelayTime => attackDelayTime;

}

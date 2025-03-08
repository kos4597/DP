using UnityEngine;

/// <summary>
/// cs���� ���� �� UTF8�� ��������� ���� �ϴ°� ����
/// UTF8�� �ƴϸ� �ν����ͳ� git ���� ������ �ѱ��� �� ������ �ּ��� �ϳ��� �Ⱥ���
/// ���� ����� ��� �ȳ��Ƿ� ã�Ƽ� �Ͻñ�....
/// </summary>
[SerializeField]
public class MonsterData
{
    [SerializeField]
    private float patrolRange = 10f; // ���� �ݰ�
    public float PatrolRange => patrolRange;

    [SerializeField]
    private float trackingRange = 5f; // �߰� �ݰ�
    public float TrackingRange => trackingRange;

    [SerializeField]
    private float moveSpeed = 4f; // �̵� �ӵ�
    public float MoveSpeed => moveSpeed;

    [SerializeField]
    private float rotationSpeed = 5f; // ȸ�� �ӵ�
    public float RotationSpeed => rotationSpeed;

    [SerializeField]
    private double hp = 100;
    public double HP => hp;

    [SerializeField]
    private float gravity = -9.81f; // �߷�
    public float Gravity => gravity;

    [SerializeField]
    private float raycastDistance = 0.5f; // �� üũ�� Raycast �Ÿ�
    public float RaycastDistance => raycastDistance;
}

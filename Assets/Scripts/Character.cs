using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera = null;

    private float rotationSpeed = 5f;
    
    // Update is called once per frame
    void Update()
    {
        RotateByMousePosition();
    }
    void RotateByMousePosition()
    {
        // ���콺 �������� �����ɴϴ�.
        Vector3 mousePosition = Input.mousePosition;

        // ���콺 �������� ���� ��ǥ�� ��ȯ�մϴ�.
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // Ray�� �浹�� ������ ���� ��ǥ�� �����ɴϴ�.
            Vector3 targetPosition = hit.point;

            // ��� ���� ���
            Vector3 direction = targetPosition - transform.position;

            // ȸ���ϵ��� ���� ����
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }
        }
    }
}

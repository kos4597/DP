using UnityEngine;

public class CharacterCamera : MonoBehaviour
{

    [SerializeField]
    private Transform target;  // ĳ���͸� ���󰡴� Ÿ��
    [SerializeField]
    private float distance = 3.0f;  // �⺻ �Ÿ�
    private float zoomSpeed = 2.0f;  // �� �ӵ�
    private float rotationSpeed = 100.0f;  // ȸ�� �ӵ�
    [SerializeField]
    private float targetOffsetY = 3.0f;
    [SerializeField]
    private float targetOffsetZ = -2.5f;
    [SerializeField]
    private float currentZoom;
    [SerializeField]
    private float currentRotation;

    void Start()
    {
        currentZoom = distance;
    }

    void Update()
    {
        CalcCamera();
    }

    private void CalcCamera()
    {
        // �� (���콺 ��)
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, 2.0f, 15.0f);  // �� ����

        // ���콺 ������ ��ư ȸ��
        if (Input.GetMouseButton(1))
        {
            currentRotation += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        }

        // ī�޶� ��ġ ������Ʈ
        Quaternion rotation = Quaternion.Euler(0, currentRotation, 0);
        Vector3 position = target.position - (rotation * Vector3.forward * currentZoom);
        position.y = position.y + targetOffsetY;
        position.z = position.z + targetOffsetZ;

        transform.position = position;
        transform.LookAt(target);
    }

}

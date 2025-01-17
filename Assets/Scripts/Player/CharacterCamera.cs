using UnityEngine;
using UnityEngine.InputSystem;

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

    private float _xRotation;
    private float _yRotation;

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
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        _yRotation += mouseX;
        _xRotation -= mouseY;

        // limits camera rotation
        _xRotation = Mathf.Clamp(_xRotation, -60f, 60f);
        _yRotation = Mathf.Clamp(_yRotation, -90f, 90f);

        Quaternion rotation = Quaternion.Euler(_xRotation, _yRotation, 0);

        // rotates camera on the y- and x-axis

        transform.rotation = rotation;
        
        // ���콺 ������ ��ư ȸ��

            //currentRotation += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        

        // ī�޶� ��ġ ������Ʈ
        Vector3 position = target.position - (rotation * Vector3.forward * currentZoom);
        position.y = position.y + targetOffsetY;
        position.z = position.z + targetOffsetZ;

        transform.position = position;
        transform.LookAt(target);
    }

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

}

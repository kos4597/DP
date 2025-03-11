using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterCamera : MonoBehaviour
{
    private Transform target;  // 캐릭터를 따라가는 타겟

    private float distance = 3.0f;  // 기본 거리
    private float zoomSpeed = 2.0f;  // 줌 속도
    private float targetOffsetY = 3.0f;
    private float targetOffsetZ = -2.5f;
    private float currentZoom;

    private float _xRotation;
    private float _yRotation;

    private Quaternion rotation = Quaternion.identity;


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
        // 줌 (마우스 휠)
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, 2.0f, 15.0f);  // 줌 제한

        if(Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            _yRotation += mouseX;
            _xRotation -= mouseY;

            _xRotation = Mathf.Clamp(_xRotation, -20f + currentZoom, 60f - currentZoom);
            rotation = Quaternion.Euler(_xRotation, _yRotation, 0);
        }

        // 카메라 위치 업데이트
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

    private void CalcCameraRot()
    {

    }

}

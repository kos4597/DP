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
        // 마우스 포지션을 가져옵니다.
        Vector3 mousePosition = Input.mousePosition;

        // 마우스 포지션을 월드 좌표로 변환합니다.
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // Ray가 충돌한 지점의 월드 좌표를 가져옵니다.
            Vector3 targetPosition = hit.point;

            // 대상 방향 계산
            Vector3 direction = targetPosition - transform.position;

            // 회전하도록 방향 지정
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }
        }
    }
}

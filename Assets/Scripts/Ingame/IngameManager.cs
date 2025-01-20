using UnityEngine;

public class IngameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player = null;
    [SerializeField]
    private Transform spawnPoint = null;
    [SerializeField]
    private CharacterCamera characterCamera = null;

    private void Start()
    {
        PlayerController p = Instantiate(player, spawnPoint).GetComponent<PlayerController>();
        p.SetPlayerCamera();
        SetCameraTarget(p.transform);
    }

    private void SetCameraTarget(Transform tr)
    {
        characterCamera.SetTarget(tr);
    }
}

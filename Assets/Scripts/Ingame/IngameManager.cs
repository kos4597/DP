using UnityEngine;
using System.Collections.Generic;

public class IngameManager : MonoBehaviour
{
    [SerializeField]
    private CharacterCamera characterCamera = null;
    [SerializeField]
    private GameObject player = null;
    [SerializeField]
    private Transform playerSpawnPoint = null;
    [SerializeField]
    private GameObject monster = null;
    [SerializeField]
    private Transform[] monsterSpawnPoints = null;
    public List<Monster> monsterPool = new List<Monster>();

    public static IngameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        PlayerController p = Instantiate(player, playerSpawnPoint).GetComponent<PlayerController>();
        p.SetPlayerCamera();
        SetCameraTarget(p.transform);
    }

    private void SpawnaMonster()
    {

    }

    private void SetCameraTarget(Transform tr)
    {
        characterCamera.SetTarget(tr);
    }
}

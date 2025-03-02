using System;
using System.Collections.Generic;
using UnityEngine;

public class IngameManager : MonoBehaviour
{
    [SerializeField]
    private CharacterCamera characterCamera = null;
    [SerializeField]
    private GameObject playerGo = null;
    [SerializeField]
    private Transform playerSpawnPoint = null;
    [SerializeField]
    private GameObject monsterGo = null;
    [SerializeField]
    private Transform monsterSpawnPoint = null;

    private Player player = null;
    private Monster monster = null;

    [NonSerialized]
    public List<Monster> monsterPool = new List<Monster>();

    public static IngameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SpawnPlayer();
        SpawnaMonster();
    }

    private void SpawnPlayer()
    {
        player = Instantiate(playerGo, playerSpawnPoint.position, Quaternion.identity).GetComponent<Player>();
        SetCameraTarget(player.transform);
    }

    private void SpawnaMonster()
    {
        monster = Instantiate(monsterGo, monsterSpawnPoint.position, Quaternion.identity).GetComponent<Monster>();

        monster.SetSpawnPoint(monsterSpawnPoint);
        monster.SetTargetPlayer(player.transform);

    }

    private void SetCameraTarget(Transform tr)
    {
        characterCamera.SetTarget(tr);
    }
}

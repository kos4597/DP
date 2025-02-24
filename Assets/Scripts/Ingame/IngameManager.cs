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
        Player player = Instantiate(playerGo, playerSpawnPoint.position, Quaternion.identity).GetComponent<Player>();
        SetCameraTarget(player.transform);
    }

    private void SpawnaMonster()
    {
        Monster monster = Instantiate(monsterGo, monsterSpawnPoint.position, Quaternion.identity).GetComponent<Monster>();
    }

    private void SetCameraTarget(Transform tr)
    {
        characterCamera.SetTarget(tr);
    }
}

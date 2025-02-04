using UnityEngine;
using System.Collections.Generic;
using System;

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
        Player p = Instantiate(player, playerSpawnPoint.position, Quaternion.identity).GetComponent<Player>();
        SetCameraTarget(p.transform);
    }

    private void SpawnaMonster()
    {
        Monster m = Instantiate(monster, monsterSpawnPoint.position, Quaternion.identity).GetComponent<Monster>();
    }

    private void SetCameraTarget(Transform tr)
    {
        characterCamera.SetTarget(tr);
    }
}

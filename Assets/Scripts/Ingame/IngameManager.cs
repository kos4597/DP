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

    [SerializeField]
    private SkillScriptableObj skillSO = null;
    public Dictionary<string , SkillData> skillSet = new Dictionary<string, SkillData>();

    [NonSerialized]
    public List<Monster> monsterPool = new List<Monster>();

    public static IngameManager Instance;

    private void Awake()
    {
        Instance = this;
        SetSkill();
    }

    private void Start()
    {
        SpawnPlayer();
        SpawnaMonster();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            SpawnaMonster();
        }
    }

    private void SetSkill()
    {
        for(int i = 0; i < skillSO.SkillDataBase.Length; i++)
        {
            skillSet.Add($"{i+1}", skillSO.SkillDataBase[i]);
        }
    }

    private void SpawnPlayer()
    {
        player = ResourceManager.CreateGameObject(StringDefine.PLAYER_PREFAB, playerSpawnPoint.position, Quaternion.identity).GetComponent<Player>();
        SetCameraTarget(player.transform);
    }

    private void SpawnaMonster()
    {
        monster = ResourceManager.CreateGameObject(StringDefine.MONSTER_PREFAB, monsterSpawnPoint.position, Quaternion.identity).GetComponent<Monster>();

        monster.SetSpawnPoint(monsterSpawnPoint);
        monster.SetTargetPlayer(player.transform);

        monsterPool.Add(monster);
    }

    private void SetCameraTarget(Transform tr)
    {
        characterCamera.SetTarget(tr);
    }
}

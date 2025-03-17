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

    public Dictionary<string ,SkillBase> skillSet = new Dictionary<string, SkillBase>();

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
        SetSkill();
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
        skillSet.Add("1", new FireBallSkill());
        skillSet.Add("2", new BulletSkill());
        skillSet.Add("3", new ArrowSKill());
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

    }

    private void SetCameraTarget(Transform tr)
    {
        characterCamera.SetTarget(tr);
    }
}

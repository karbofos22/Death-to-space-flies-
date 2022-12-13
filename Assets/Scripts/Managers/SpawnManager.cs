using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnManager : MonoBehaviour
{
    #region Fields
    private readonly float offset = 60;
    private float playerPosByZ;
    const int border = 41;

    private int laserSpawnChance;
    private bool isCanSpawn;
    private bool isBossTime;


    [Header("Obstacles")]
    public List<Rigidbody> asteroids;
    private LayerMask obstacleLayer;

    [Header("Enemies")]
    public GameObject lightFly;
    public GameObject heavyFly;
    public int lightFliesCount = 7;
    public int heavyFliesCount = 3;
    private LayerMask enemyLayer;

    [Header("PowerUps")]
    public GameObject beamchargePowerUp;
    public GameObject hpPowerUp;
    public GameObject laserPowerUp;
    private LayerMask powerUpLayer;

    [Header("Boss")]
    public GameObject boss;
    private Vector3 bossSpawnPos = new(0, 0, 85f);
    #endregion

    private void Start()
    {
        obstacleLayer = LayerMask.GetMask("Obstacle");
        powerUpLayer = LayerMask.GetMask("PowerUp");
        enemyLayer = LayerMask.GetMask("Enemy");
        isCanSpawn = false;
        isBossTime = false;

        GlobalEventManager.GameIsActive.AddListener(GameActive);
        GlobalEventManager.BossIncoming.AddListener(BossIncoming);
        GlobalEventManager.GameOver.AddListener(GameOver);
        GlobalEventManager.BossDead.AddListener(BossDefeated);

        InvokeRepeating(nameof(SpawnLaserPowerUp), 1, 10);
        InvokeRepeating(nameof(SpawnBeamChargePowerUp), 1, 9);
        InvokeRepeating(nameof(SpawnHpPowerUp), 9, 15);
        InvokeRepeating(nameof(SpawnLightFly), 2, 3);
        InvokeRepeating(nameof(SpawnHeavyFly), 2, 4);
        InvokeRepeating(nameof(SpawnObstacle), 2, 8.4f);
    }

    private void Update()
    {
        GetPlayerPos();
        SpawnChanceRandom();
    }

    #region Methods
    private void GameActive()
    {
        isCanSpawn = true;
    }
    private void BossIncoming()
    {
        SpawnBoss();
    }
    private void GameOver()
    {
        isCanSpawn = false;
    }
    private void BossDefeated()
    {
        isBossTime = false;
    }
    private void SpawnObstacle()
    {
        if (isCanSpawn)
        {
            Vector3 spawnPos = RandomSpawnPos();
            if (!Physics.CheckSphere(spawnPos, 2f, obstacleLayer))
            {
                Instantiate(asteroids[Random.Range(0, asteroids.Count)], spawnPos, transform.rotation);
            }
        }
    }
    private void SpawnBeamChargePowerUp()
    {
        if (isCanSpawn)
        {
            Vector3 spawnPos = RandomSpawnPos();
            if (!Physics.CheckSphere(spawnPos, 2.5f, powerUpLayer))
            {
                Instantiate(beamchargePowerUp, spawnPos, Quaternion.identity);
            }
        }
    }
    private void SpawnHpPowerUp()
    {
        if (isCanSpawn)
        {
            Vector3 spawnPos = RandomSpawnPos();
            if (!Physics.CheckSphere(spawnPos, 2.5f, powerUpLayer))
            {
                Instantiate(hpPowerUp, spawnPos, Quaternion.identity);
            }
        }
    }
    private void SpawnLaserPowerUp()
    {
        if (isCanSpawn && laserSpawnChance > 60)
        {
            Vector3 spawnPos = RandomSpawnPos();
            Instantiate(laserPowerUp, spawnPos, Quaternion.identity);
        }
    }
    private void SpawnLightFly()
    {
        if (!isBossTime && isCanSpawn)
        {
            Vector3 spawnPos = RandomSpawnPos();

            if (GameObject.FindGameObjectsWithTag("LightFly").Length < lightFliesCount)
            {
                Instantiate(lightFly, spawnPos, lightFly.transform.rotation);
            }
        }
    }
    private void SpawnHeavyFly()
    {
        if (!isBossTime && isCanSpawn)
        {
            Vector3 spawnPos = RandomSpawnPos();

            if (GameObject.FindGameObjectsWithTag("HeavyFly").Length < heavyFliesCount && !Physics.CheckSphere(spawnPos, 3.2f, enemyLayer))
            {
                Instantiate(heavyFly, spawnPos, heavyFly.transform.rotation);
            }
        }
    }
    private void SpawnBoss()
    {
        if (!isBossTime)
        {
            Instantiate(boss, bossSpawnPos, boss.transform.rotation);
        }
        isBossTime = true;
    }
    private void GetPlayerPos()
    {
        if (isCanSpawn)
        {
            playerPosByZ = GameObject.Find("Player").transform.position.z;
        }
    }
    private void SpawnChanceRandom()
    {
        laserSpawnChance = Random.Range(0, 100);
    }
    private Vector3 RandomSpawnPos()
    {
        Vector3 spawnPos = new(Random.Range(-border, border), 0, offset + playerPosByZ);
        return spawnPos;
    }
    #endregion
}

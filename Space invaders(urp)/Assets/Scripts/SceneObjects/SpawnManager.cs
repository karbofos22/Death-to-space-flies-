using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //Coordination fields
    float offset;
    const int border = 41;

    //Another scripts
    private GameManager gameManager;

    //Etc.
    private int laserSpawnChance;

    [Header("Obstacles")]
    public List<Rigidbody> asteroids;
    const int maxObstacleCount = 4;
    private LayerMask obstacleLayer;

    [Header("Enemies")]
    public GameObject lightFly;
    public GameObject heavyFly;
    private int lightFliesCount = 7;
    private int heavyFliesCount = 3;
    private LayerMask enemyLayer;

    [Header("PowerUps")]
    public GameObject beamchargePowerUp;
    public GameObject hpPowerUp;
    public GameObject laserPowerUp;
    private LayerMask powerUpLayer;

    [Header("Boss")]
    public GameObject boss;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        obstacleLayer = LayerMask.GetMask("Obstacle");
        powerUpLayer = LayerMask.GetMask("PowerUp");
        enemyLayer = LayerMask.GetMask("Enemy");

        InvokeRepeating(nameof(SpawnLaserPowerUp), 1, 10);
        InvokeRepeating(nameof(SpawnBeamChargePowerUp), 1, 9);
        InvokeRepeating(nameof(SpawnHpPowerUp), 9, 15);
        InvokeRepeating(nameof(SpawnLightFly), 2, 3);
        InvokeRepeating(nameof(SpawnHeavyFly), 2, 4);
        InvokeRepeating(nameof(SpawnObstacle), 2, 4);

        StartCoroutine(SpawnBoss());
    }

    void Update()
    {
        if (gameManager.isGameActive)
        {
            GetOffsetFromPlayer();
            SpawnChanceRandom();
        }
    }

    #region Methods
    void SpawnObstacle()
    {
        if (GameObject.FindGameObjectsWithTag("Obstacle").Length < maxObstacleCount && gameManager.isGameActive)
        {
            Vector3 spawnPos = RandomSpawnPos();
            if (!Physics.CheckSphere(spawnPos, 2f, obstacleLayer))
            {
                Instantiate(asteroids[Random.Range(0, asteroids.Count)], spawnPos, transform.rotation);
            }
        }
    }
    void SpawnBeamChargePowerUp()
    {
        if (gameManager.isGameActive)
        {
            Vector3 spawnPos = RandomSpawnPos();
            if (!Physics.CheckSphere(spawnPos, 2.5f, powerUpLayer))
            {
                Instantiate(beamchargePowerUp, spawnPos, Quaternion.identity);
            }
        }
    }
    void SpawnHpPowerUp()
    {
        if (gameManager.isGameActive)
        {
            Vector3 spawnPos = RandomSpawnPos();
            if (!Physics.CheckSphere(spawnPos, 2.5f, powerUpLayer))
            {
                Instantiate(hpPowerUp, spawnPos, Quaternion.identity);
            }
        }
    }
    void SpawnLaserPowerUp()
    {
        if (gameManager.isGameActive && laserSpawnChance > 60)
        {
            Vector3 spawnPos = RandomSpawnPos();
            Instantiate(laserPowerUp, spawnPos, Quaternion.identity);
        }
    }
    void SpawnLightFly()
    {
        if (!gameManager.isBossTime)
        {
            Vector3 spawnPos = RandomSpawnPos();

            if (GameObject.FindGameObjectsWithTag("LightFly").Length < lightFliesCount && gameManager.isGameActive)
            {
                Instantiate(lightFly, spawnPos, lightFly.transform.rotation);

                lightFliesCount++;
            }
        }
    }
    void SpawnHeavyFly()
    {
        if (!gameManager.isBossTime)
        {
            Vector3 spawnPos = RandomSpawnPos();

            if (GameObject.FindGameObjectsWithTag("HeavyFly").Length < heavyFliesCount && gameManager.isGameActive && !Physics.CheckSphere(spawnPos, 3.2f, enemyLayer))
            {
                Instantiate(heavyFly, spawnPos, heavyFly.transform.rotation);

                heavyFliesCount++;
            }
        }
    }
    IEnumerator SpawnBoss()
    {
        yield return new WaitUntil(() => gameManager.isBossTime);

        Vector3 spawnPos = new(0, 0, 85f);

        Instantiate(boss, spawnPos, boss.transform.rotation);
    }
    void GetOffsetFromPlayer()
    {
        offset = GameObject.Find("Player").transform.position.z;
    }
    void SpawnChanceRandom()
    {
        laserSpawnChance = Random.Range(0, 100);
    }
    Vector3 RandomSpawnPos()
    {
        Vector3 spawnPos = new(Random.Range(-border, border), 0, 60 + offset);
        return spawnPos;
    }
    #endregion
}

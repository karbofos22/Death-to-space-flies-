using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeBossBehaviour : MonoBehaviour
{
    #region Boss fields
    private int hp;
    const int scoreValue = 20000;
    const float moveSpeed = 3f;
    const float strafeSpeed = 3f;
    const float strafeDelta = 11f;
    private Vector3 stopPoint = new(0, 0, 43.5f);
    #endregion

    #region Boss statuses
    private bool isCanStrafe;
    private bool isCanShoot;
    private bool isActive;
    #endregion

    #region Prefabs/Objects
    public Rigidbody projectileRb;
    public GameObject rayProjectile;
    public List<Transform> firePoints;
    #endregion

    #region Shooting fields
    const float fireRate = 3f;
    private float nextShot;
    private Vector3 shootPoint;
    private EnemyProjectile enemyProjectile;
    private Vector3 player;
    #endregion

    #region Material switch
    private Material whiteMat;
    private Material defaultMat;
    private MeshRenderer meshRenderer;
    #endregion

    #region Scripts
    private GameManager gameManager;
    #endregion

    void Start()
    {
        enemyProjectile = projectileRb.GetComponent<EnemyProjectile>();

        meshRenderer = GetComponent<MeshRenderer>();
        whiteMat = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        defaultMat = meshRenderer.material;

        isCanShoot = false;
        isCanStrafe = false;
        isActive = true;

        StartCoroutine(Setup());
        StartCoroutine(LaserRayShoot());
        StartCoroutine(TemporaryImmortality());
    }

    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.startBossInfo = true;
    }
    void Update()
    {
        if (gameManager.isGameActive)
        {
            Movement();
            Shoot();
        }
    }

    #region Methods
    void Movement()
    {
        MoveToPos();

        if (isCanStrafe)
        {
            transform.position = StrafeByX();
        }
    }
    Vector3 StrafeByX()
    {
        Vector3 v = stopPoint;
        v.x += strafeDelta * Mathf.Sin(Time.time * strafeSpeed);
        v.y = stopPoint.y;
        v.z = transform.position.z;

        return v;
    }
    void Shoot()
    {
        if (Time.time > fireRate + nextShot && isCanShoot)
        {
            foreach (var firePoint in firePoints)
            {
                Rigidbody shot = Instantiate(projectileRb, firePoint.transform.position, projectileRb.transform.rotation);
                shot.velocity = enemyProjectile.projectileSpeed * Time.deltaTime * GetShootPoint();

                nextShot = Time.time;

                if (shot.velocity.magnitude > enemyProjectile.projectileSpeed || shot.velocity.magnitude < enemyProjectile.projectileSpeed)
                {
                    shot.velocity = shot.velocity.normalized * enemyProjectile.projectileSpeed;
                }
            }
        }
    }
    IEnumerator TemporaryImmortality()
    {
        while (!isCanStrafe)
        {
            hp = 1000000;
            yield return null;
        }
    }
    IEnumerator LaserRayShoot()
    {
        yield return new WaitUntil(() => isCanStrafe);

        while (isActive)
        {
            yield return new WaitForSeconds(10);
            rayProjectile.SetActive(true);
            yield return new WaitForSeconds(4);
            rayProjectile.SetActive(false);
        }
    }
    IEnumerator Setup()
    {
        yield return new WaitUntil(() => transform.position == stopPoint);

            hp = 9500;
            isCanStrafe = true;
            isCanShoot = true;
            gameManager.startBossInfo = false;
    }
    void MoveToPos()
    {
        transform.position = Vector3.MoveTowards(transform.position, stopPoint, moveSpeed * Time.deltaTime);
    }
    Vector3 GetShootPoint()
    {
        return shootPoint = -transform.position + PlayerPos();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            TakeDamage(LaserProjectile.damageAmount);
            meshRenderer.material = whiteMat;
        }
        if (other.CompareTag("Beam"))
        {
            TakeDamage(BeamProjectile.damageAmount);
            meshRenderer.material = whiteMat;
        }
        if (other.CompareTag("Obstacle"))
        {
            TakeDamage(Obstacle.damageAmount);
            Destroy(other.gameObject);
            meshRenderer.material = whiteMat;
        }
    }
    public void TakeDamage(int amount)
    {
        hp -= amount;
        if (hp <= 0)
        {
            isActive = false;
            Destroy(gameObject);
            gameManager.ScoresIncrease(scoreValue);
            gameManager.isBossTime = false;
        }
        else
        {
            Invoke(nameof(ResetMat), .15f);
        }
    }
    void ResetMat()
    {
        meshRenderer.material = defaultMat;
    }
    Vector3 PlayerPos()
    {
        return player = GameObject.Find("Player").transform.position;
    }
    #endregion
}

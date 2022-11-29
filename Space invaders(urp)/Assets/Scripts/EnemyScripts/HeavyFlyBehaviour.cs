using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyFlyBehaviour : MonoBehaviour
{
    #region Heavy fly stat fields
    private int hp = 850;
    const int scoreValue = 200;
    const float moveSpeed = 1f;
    public ParticleSystem bloodParticle;
    #endregion

    #region Prefabs/Objects
    public Rigidbody projectileRb;
    public List<Transform> firePoints;
    #endregion

    #region Shooting fields
    const float fireRate = 5f;
    private bool isCanShoot;
    float nextShot;
    private EnemyProjectile enemyProjectile;
    private Vector3 player;
    private Vector3 shootPoint;
    #endregion

    #region Raycast fields
    readonly float rayDistance = 80f;
    RaycastHit hit;
    #endregion

    #region Scripts
    private GameManager gameManager;
    #endregion

    void Start()
    {
        enemyProjectile = projectileRb.GetComponent<EnemyProjectile>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        isCanShoot = true;
    }

    void Update()
    {
        if (gameManager.isGameActive)
        {
            RayView();
            Movement();
            Shoot();
        }
    }

    #region Methods
    void Movement()
    {
        transform.LookAt(PlayerPos());
        transform.position = Vector3.MoveTowards(transform.position, GameObject.Find("Destroyer").transform.position, moveSpeed * Time.deltaTime);
    }
    void Shoot()
    {
        if (Time.time > fireRate + nextShot && isCanShoot)
        {
            foreach (var firePoint in firePoints)
            {
                Rigidbody shot = Instantiate(projectileRb, firePoint.transform.position, projectileRb.transform.rotation);
                shot.velocity = enemyProjectile.projectileSpeed * Time.deltaTime * shootPoint;

                nextShot = Time.time;

                if (shot.velocity.magnitude > enemyProjectile.projectileSpeed || shot.velocity.magnitude < enemyProjectile.projectileSpeed)
                {
                    shot.velocity = shot.velocity.normalized * enemyProjectile.projectileSpeed;
                }
            }
        }
        if (transform.position.z <= 2)
        {
            isCanShoot = false;
        }
    }
    void RayView()
    {
        Ray ray = new(transform.position, transform.forward);

        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            shootPoint = -transform.position + hit.point;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            TakeDamage(LaserProjectile.damageAmount);
        }
        if (other.CompareTag("Obstacle"))
        {
            TakeDamage(Obstacle.damageAmount);
            Destroy(other.gameObject);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Beam"))
        {
            TakeDamage(BeamProjectile.damageAmount);
        }
    }
    public void TakeDamage(int amount)
    {
        hp -= amount;
        if (hp < 100)
        {
            bloodParticle.Emit(240);
        }
        if (hp <= 0)
        {
            Destroy(gameObject);
            gameManager.ScoresIncrease(scoreValue);
        }
        else
        {
            bloodParticle.Play();
        }
    }
    Vector3 PlayerPos()
    {
        return player = GameObject.Find("Player").transform.position;
    }
    #endregion
}

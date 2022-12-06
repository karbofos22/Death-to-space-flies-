using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlyBehaviour : MonoBehaviour
{
    #region Light fly stat fields
    private int hp = 200;
    const int scoreValue = 100;
    const float moveSpeed = 5.5f;
    private Vector3 startPosX;
    const float strafeSpeed = 4;
    const float strafeDelta = 3.5f;
    private bool isCanShoot;
    public ParticleSystem bloodParticle;
    #endregion

    #region Prefabs/Objects
    public Rigidbody projectileRb;
    public Transform firePoint;
    #endregion

    #region Shooting fields
    const float fireRate = 2f;
    float nextShot;
    private Vector3 shootPoint;
    private EnemyProjectile enemyProjectile;
    private Vector3 player;
    #endregion

    #region Raycast fields
    readonly float rayDistance = 65f;
    RaycastHit hit;
    #endregion

    #region Material switcher
    private Material whiteMat;
    private Material defaultMat;
    MeshRenderer meshRenderer;
    #endregion

    #region Scripts
    private GameManager gameManager;
    #endregion

    void Start()
    {
        enemyProjectile = projectileRb.GetComponent<EnemyProjectile>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        startPosX = transform.position;

        meshRenderer = GetComponent<MeshRenderer>();
        whiteMat = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        defaultMat = meshRenderer.material;

        isCanShoot = true;
    }

    void Update()
    {
        if (gameManager.isGameActive)
        {
            RayView();

            Shoot();
            Movement();
        }
    }
    
    #region Methods 
    void Movement()
    {
        transform.LookAt(PlayerPos());
        transform.position = Vector3.MoveTowards(StrafeByX(), GameObject.Find("Destroyer").transform.position, moveSpeed * Time.deltaTime);
    }
    Vector3 StrafeByX()
    {
        Vector3 v = startPosX;
        v.x += strafeDelta * Mathf.Sin(Time.time * strafeSpeed);
        v.y = startPosX.y;
        v.z = transform.position.z;

        return v;
    }
    void Shoot()
    {
        if (Time.time > fireRate + nextShot && isCanShoot)
        {
            Rigidbody shot = Instantiate(projectileRb, firePoint.transform.position, projectileRb.transform.rotation);
            shot.velocity = enemyProjectile.projectileSpeed * Time.deltaTime * shootPoint;

            nextShot = Time.time;

            if (shot.velocity.magnitude > enemyProjectile.projectileSpeed || shot.velocity.magnitude < enemyProjectile.projectileSpeed)
            {
                shot.velocity = shot.velocity.normalized * enemyProjectile.projectileSpeed;
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
        if (hp < 50)
        {
            bloodParticle.Emit(60);
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

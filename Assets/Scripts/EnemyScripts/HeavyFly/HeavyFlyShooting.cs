using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyFlyShooting : MonoBehaviour
{
    #region Shooting fields
    [SerializeField] private float fireRate = 5f;
    private bool isCanShoot;
    private float nextShot;
    private Vector3 shootPoint;
    #endregion

    #region Shooting Prefabs/Objects
    public Rigidbody projectileRb;
    public List<Transform> firePoints;
    #endregion

    #region Raycast fields
    private readonly float rayDistance = 80f;
    private RaycastHit hit;
    #endregion

    private void Start()
    {
        isCanShoot = true;
        GlobalEventManager.GameOver.AddListener(StopShootingIfGameOver);
    }
    private void Update()
    {
        if (isCanShoot)
        {
            transform.LookAt(PlayerPos());
            FindTarget();
            Shoot();
        }
    }

    #region Methods
    private void Shoot()
    {
        if (Time.time > fireRate + nextShot)
        {
            foreach (var firePoint in firePoints)
            {
                Rigidbody shot = Instantiate(projectileRb, firePoint.transform.position, projectileRb.transform.rotation);
                shot.velocity = EnemyProjectile.speed * Time.deltaTime * shootPoint;

                nextShot = Time.time;

                if (shot.velocity.magnitude > EnemyProjectile.speed || shot.velocity.magnitude < EnemyProjectile.speed)
                {
                    shot.velocity = shot.velocity.normalized * EnemyProjectile.speed;
                }
            }
        }
        if (transform.position.z <= 2)
        {
            isCanShoot = false;
        }
    }
    private void FindTarget()
    {
        Ray ray = new(transform.position, transform.forward);

        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            shootPoint = -transform.position + hit.point;
        }
    }
    private Vector3 PlayerPos()
    {
        return GameObject.Find("Player").transform.position;
    }
    private void StopShootingIfGameOver()
    {
        isCanShoot = false;
        GlobalEventManager.GameOver.RemoveListener(StopShootingIfGameOver);
    }
    #endregion
}

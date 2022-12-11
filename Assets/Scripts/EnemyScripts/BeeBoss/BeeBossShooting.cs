using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeBossShooting : MonoBehaviour
{
    #region Shooting fields
    [SerializeField] private float fireRate = 3f;
    private float nextShot;
    private Vector3 shootPoint;
    public bool laserRayActive;
    private bool isCanShoot;
    #endregion

    #region Shooting Prefabs/Objects
    public Rigidbody projectileRb;
    public GameObject rayProjectile;
    public List<Transform> firePoints;
    #endregion

    void Start()
    {
        isCanShoot = true;

        AddGameOverListener();
        StartCoroutine(LaserRayShoot());
    }
    void Update()
    {
        if (isCanShoot)
        {
            Shoot();
        }
    }
    #region Methods
    private void AddGameOverListener()
    {
        GlobalEventManager.GameOver.AddListener(() =>
        {
            isCanShoot = false;
        });
    }
    IEnumerator LaserRayShoot()
    {
        yield return new WaitUntil(() => laserRayActive);

        while (laserRayActive)
        {
            yield return new WaitForSeconds(10);
            rayProjectile.SetActive(true);
            yield return new WaitForSeconds(4);
            rayProjectile.SetActive(false);
        }
    }
    void Shoot()
    {
        if (Time.time > fireRate + nextShot)
        {
            foreach (var firePoint in firePoints)
            {
                Rigidbody shot = Instantiate(projectileRb, firePoint.transform.position, projectileRb.transform.rotation);
                shot.velocity = EnemyProjectile.speed * Time.deltaTime * GetShootPoint();

                nextShot = Time.time;

                if (shot.velocity.magnitude > EnemyProjectile.speed || shot.velocity.magnitude < EnemyProjectile.speed)
                {
                    shot.velocity = shot.velocity.normalized * EnemyProjectile.speed;
                }
            }
        }
    }
    Vector3 GetShootPoint()
    {
        return shootPoint = -transform.position + PlayerPos();
    }
    Vector3 PlayerPos()
    {
        return GameObject.Find("Player").transform.position;
    }
    #endregion
}

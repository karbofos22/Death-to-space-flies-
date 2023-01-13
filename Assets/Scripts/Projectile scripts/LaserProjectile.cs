using System;
using UnityEngine;

public class LaserProjectile : MonoBehaviour
{
    #region Fields
    private Action<LaserProjectile> _killAction;

    private Rigidbody projectileRb;

    public const int damageAmount = 6;
    const float projectileSpeed = 100f;
    #endregion

    void Start()
    {
        projectileRb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        projectileRb.velocity = Vector3.forward * projectileSpeed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TopBorder")
            || other.CompareTag("EnemyProjectile")
            || other.CompareTag("LightFly")
            || other.CompareTag("HeavyFly")
            || other.CompareTag("Obstacle")
            || other.CompareTag("PowerUp")
            || other.CompareTag("BossRay"))
        {
            _killAction(this);
        }
    }
    public void Init(Action<LaserProjectile> killAction)
    {
        _killAction = killAction;
    }
}

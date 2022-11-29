using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserProjectile : MonoBehaviour
{
    private Action<LaserProjectile> _killAction;

    private Rigidbody projectileRb;

    //Stats
    public const int damageAmount = 6;
    const float projectileSpeed = 100f;

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

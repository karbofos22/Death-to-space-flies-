using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private LaserProjectile projectile;
    public ObjectPool<LaserProjectile> pool;

    private void Start()
    {
        pool = new ObjectPool<LaserProjectile>(() =>
        {
            return Instantiate(projectile);
        }, shot =>
        {
            shot.gameObject.SetActive(true);
        }, shot =>
        {
            shot.gameObject.SetActive(false);
        }, shot =>
        {
            Destroy(shot.gameObject);
        }, true, 30, 100);

    }
    public void Kill(LaserProjectile shot)
    {
        pool.Release(shot);
    }
}

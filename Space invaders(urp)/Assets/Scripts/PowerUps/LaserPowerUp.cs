using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPowerUp : MonoBehaviour
{
    private int hp = 300;
    const float PowerUpTimeAmount = 30;

    private bool hasEntered;

    private LaserWeapons laserWeapons;

    private Material whiteMat;
    private Material defaultMat;
    MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();

        whiteMat = Resources.Load("WhiteFlash", typeof(Material)) as Material;

        defaultMat = meshRenderer.material;

        laserWeapons = GameObject.Find("Player").GetComponent<LaserWeapons>();
    }
    void Update()
    {
        transform.Translate(Vector3.back * 0.06f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasEntered)
        {
            hasEntered = true;
            laserWeapons.isPowerUpActive = true;
            laserWeapons.laserPowerUpLifeTime = PowerUpTimeAmount;
            Destroy(gameObject);
        }
        if (other.CompareTag("Projectile"))
        {
            TakeDamage(LaserProjectile.damageAmount);
            meshRenderer.material = whiteMat;
        }
        if (other.CompareTag("EnemyProjectile"))
        {
            TakeDamage(EnemyProjectile.damageAmount);
            meshRenderer.material = whiteMat;
        }
        if (other.CompareTag("Obstacle"))
        {
            TakeDamage(Obstacle.damageAmount);
            meshRenderer.material = whiteMat;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Beam"))
        {
            TakeDamage(BeamProjectile.damageAmount);
            meshRenderer.material = whiteMat;
        }
    }
    public void TakeDamage(int amount)
    {
        hp -= amount;
        if (hp <= 0)
        {
            Destroy(gameObject);
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamRayChargeUnit : MonoBehaviour
{
    readonly int powerUpAmount = 35;
    public int hp = 300;

    private bool hasEntered;

    private BeamRayWeapon beamRay;

    private Material whiteMat;
    private Material defaultMat;
    MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();

        whiteMat = Resources.Load("WhiteFlash", typeof(Material)) as Material;

        defaultMat = meshRenderer.material;

        beamRay = GameObject.Find("Player").GetComponent<BeamRayWeapon>();
    }
    private void Update()
    {
        transform.Translate(Vector3.back * 0.06f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasEntered)
        {
            hasEntered = true;
            beamRay.BeamCharge(powerUpAmount);
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



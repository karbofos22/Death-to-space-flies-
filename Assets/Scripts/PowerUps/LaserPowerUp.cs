using UnityEngine;
using Zenject;

public class LaserPowerUp : MonoBehaviour
{
    #region Fields
    [SerializeField] private int hp = 300;
    [SerializeField] private float PowerUpTimeAmount = 20;

    private bool hasEntered;
    [Inject] private LaserWeapons laserWeapons;

    [SerializeField] private Material whiteMat;
    private Material defaultMat;
    private MeshRenderer meshRenderer;
    #endregion

    private void Start()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        defaultMat = meshRenderer.material;
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
            laserWeapons.isPowerUpActive = true;
            laserWeapons.laserPowerUpLifeTime = PowerUpTimeAmount;
            Destroy(gameObject);
        }
        if (other.GetComponent<LaserProjectile>())
        {
            TakeDamage(LaserProjectile.damageAmount);
            meshRenderer.material = whiteMat;
        }
        if (other.GetComponent<EnemyProjectile>())
        {
            TakeDamage(EnemyProjectile.damageAmount);
            meshRenderer.material = whiteMat;
        }
        if (other.GetComponent<Obstacle>())
        {
            TakeDamage(Obstacle.damageAmount);
            meshRenderer.material = whiteMat;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<BeamProjectile>())
        {
            TakeDamage(BeamProjectile.damageAmount);
            meshRenderer.material = whiteMat;
        }
    }
    private void TakeDamage(int amount)
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
    private void ResetMat()
    {
        meshRenderer.material = defaultMat;
    }
}

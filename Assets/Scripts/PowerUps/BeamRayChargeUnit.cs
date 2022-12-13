using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class BeamRayChargeUnit : MonoBehaviour
{
    #region Fields
    [SerializeField] private int powerUpAmount = 35;
    [SerializeField] private int hp = 300;
    private bool hasEntered;

    [SerializeField] private Material whiteMat;
    private Material defaultMat;
    private MeshRenderer meshRenderer;
    [Inject] private BeamRayWeapon beamRay;
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
            beamRay.BeamCharge(powerUpAmount);
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
    private void ResetMat()
    {
        meshRenderer.material = defaultMat;
    }
}



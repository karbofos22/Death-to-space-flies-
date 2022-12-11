using System.Collections;
using TMPro;
using UnityEngine;
public class PlayerBehaviour : MonoBehaviour
{
    #region Fields
    public int hp;
    [SerializeField] private int maxHp = 1300;
    private readonly int maxDamage = 2000;
    private WaitForSeconds timeForExplosionAnimation = new(3.6f);

    [SerializeField] private Material whiteMat;
    private Material defaultMat;
    private MeshRenderer meshRenderer;

    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private HpBar hpBar;
    #endregion
    #region Scripts
    private LaserWeapons laserWeapons;
    private BeamRayWeapon beamRay;
    private PlayerMovement playerMovement;
    #endregion

    void Start()
    {
        laserWeapons = GetComponent<LaserWeapons>();
        beamRay = GetComponent<BeamRayWeapon>();
        playerMovement = GetComponent<PlayerMovement>();

        meshRenderer = GetComponent<MeshRenderer>();
        defaultMat = meshRenderer.material;

        PrimarySetup();

        StartCoroutine(Death());
    }

    #region Methods
    private void PrimarySetup()
    {
        GlobalEventManager.GameIsActive.AddListener(() =>
        {
            hp = maxHp;
            laserWeapons.enabled = true;
            beamRay.enabled = true;
            playerMovement.enabled = true;
        });
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyProjectile"))
        {
            meshRenderer.material = whiteMat;
            TakeDamage(EnemyProjectile.damageAmount);
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Obstacle"))
        {
            meshRenderer.material = whiteMat;
            TakeDamage(Obstacle.damageAmount);
            Destroy(other.gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            TakeDamage(maxDamage);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("BossRay"))
        {
            TakeDamage(BossRayProjectile.damageAmount);
            meshRenderer.material = whiteMat;
        }
    }
    public void TakeDamage(int amount)
    {
        hp -= amount;

        if (hp <= 0)
        {
            explosion.Play();
        }
        else
        {
            Invoke(nameof(ResetMat), .13f);
        }
    }
    private void OnDeathControlsKill()
    {
        laserWeapons.enabled = false;
        beamRay.enabled = false;
        playerMovement.enabled = false;
    }
    IEnumerator Death()
    {
        yield return new WaitUntil(() => hp <= 0);

        OnDeathControlsKill();

        yield return timeForExplosionAnimation;
        
        explosion.Stop();

        GlobalEventManager.SendGameIsOver();

        Destroy(gameObject);
    }
    public void HpRestore(int hpAmount)
    {
        hp += hpAmount;
        if (hp > maxHp)
        {
            hp = maxHp;
        }
    }
    void ResetMat()
    {
        meshRenderer.material = defaultMat;
    }
    #endregion
}

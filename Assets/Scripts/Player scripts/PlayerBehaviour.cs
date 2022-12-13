using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
public class PlayerBehaviour : MonoBehaviour
{
    #region Fields
    [SerializeField] private int hp;
    [HideInInspector] public int hpHudValue;
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

    private void Start()
    {
        laserWeapons = GetComponent<LaserWeapons>();
        beamRay = GetComponent<BeamRayWeapon>();
        playerMovement = GetComponent<PlayerMovement>();

        meshRenderer = GetComponent<MeshRenderer>();
        defaultMat = meshRenderer.material;

        GlobalEventManager.GameIsActive.AddListener(PrimarySetup);

        StartCoroutine(Death());
    }

    #region Methods
    private void PrimarySetup()
    {
        hp = maxHp;
        hpHudValue = hp;
        laserWeapons.enabled = true;
        beamRay.enabled = true;
        playerMovement.enabled = true;
        GlobalEventManager.GameIsActive.RemoveListener(PrimarySetup);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyProjectile>())
        {
            meshRenderer.material = whiteMat;
            TakeDamage(EnemyProjectile.damageAmount);
            Destroy(other.gameObject);
        }
        if (other.GetComponent<Obstacle>())
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
        if (other.GetComponent<BossRayProjectile>())
        {
            TakeDamage(BossRayProjectile.damageAmount);
            meshRenderer.material = whiteMat;
        }
    }
    private void OnDeathControlsKill()
    {
        laserWeapons.enabled = false;
        beamRay.enabled = false;
        playerMovement.enabled = false;
    }
    private IEnumerator Death()
    {
        yield return new WaitUntil(() => hp <= 0);

        OnDeathControlsKill();

        yield return timeForExplosionAnimation;

        explosion.Stop();

        GlobalEventManager.SendGameIsOver();

        Destroy(gameObject);
    }
    public void TakeDamage(int amount)
    {
        hp -= amount;
        hpHudValue = hp;
        if (hp <= 0)
        {
            explosion.Play();
        }
        else
        {
            Invoke(nameof(ResetMat), .13f);
        }
    }
    public void HpRestore(int hpAmount)
    {
        hp += hpAmount;
        hpHudValue = hp;
        if (hp > maxHp)
        {
            hp = maxHp;
        }
    }
    private void ResetMat()
    {
        meshRenderer.material = defaultMat;
    }
    #endregion
}

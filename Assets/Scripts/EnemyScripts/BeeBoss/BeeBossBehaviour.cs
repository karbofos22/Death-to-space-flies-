using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeBossBehaviour : MonoBehaviour, IEnemy
{
    #region Fields
    [SerializeField] private int hp;
    private bool isActive;
    public ParticleSystem bloodParticle;
    private readonly int immortality = 1000000;
    #endregion

    #region Properties
    public int Hp
    {
        get { return hp; }
        set { hp = value; }
    }
    public int ScoreValue { get; set; }
    #endregion

    #region Scripts
    private BeeBossMovement bossMovement;
    private BeeBossShooting bossShooting;
    #endregion

    void Awake()
    {
        bossMovement = GetComponent<BeeBossMovement>();
        bossShooting = GetComponent<BeeBossShooting>();

        bossMovement.enabled = true;
    }
    void Start()
    {
        Hp = immortality;
        ScoreValue = 20000;

        PrimarySetup();
    }
    #region Methods
    private void PrimarySetup()
    {
        GlobalEventManager.BossFight.AddListener(() =>
        {
            if (!isActive)
            {
                Hp = 9500;
                isActive = true;
                bossShooting.enabled = true;
                bossShooting.laserRayActive = true;
            }
        });
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            TakeDamage(LaserProjectile.damageAmount);
        }
        if (other.CompareTag("Beam"))
        {
            TakeDamage(BeamProjectile.damageAmount);
        }
        if (other.CompareTag("Obstacle"))
        {
            TakeDamage(Obstacle.damageAmount);
            Destroy(other.gameObject);
        }
    }
    public void TakeDamage(int amount)
    {
        Hp -= amount;
        if (Hp < 500)
        {
            bloodParticle.Emit(400);
        }
        if (Hp <= 0)
        {
            GlobalEventManager.SendBossDead();
            Destroy(gameObject);
        }
        else
        {
            bloodParticle.Play();
        }
    }
    #endregion
}

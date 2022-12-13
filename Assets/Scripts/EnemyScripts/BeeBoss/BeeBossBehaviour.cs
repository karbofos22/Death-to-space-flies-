using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BeeBossBehaviour : MonoBehaviour, IEnemy
{
    #region Fields
    [SerializeField] private int hp;
    private bool isActive;
    private bool gotPoints;
    [SerializeField] private ParticleSystem bloodParticle;
    private readonly int emissionParticleToEmit = 400;
    private readonly int properHp = 9500;
    private readonly int hpLowerLimit = 500;
    private readonly int immortality = 1000000; // To easy up, temporary immortality implemented via massive Hp number
    private readonly int bossScoreValue = 20000;
    #endregion

    #region Properties
    public int Hp
    {
        get { return hp; }
        private set { hp = value; }
    }
    public int ScoreValue { get; private set; }
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
        ScoreValue = bossScoreValue;

        GlobalEventManager.BossReadyToFight.AddListener(PrimarySetup);
    }
    #region Methods
    private void PrimarySetup()
    {
        if (!isActive)
        {
            Hp = properHp;
            isActive = true;
            bossShooting.enabled = true;
            GlobalEventManager.BossReadyToFight.RemoveListener(PrimarySetup);
        }
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
        if (Hp < hpLowerLimit)
        {
            bloodParticle.Emit(emissionParticleToEmit);
        }
        if (Hp <= 0 && !gotPoints)
        {
            gotPoints = true;
            GlobalEventManager.SendEnemyKilled(ScoreValue);
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

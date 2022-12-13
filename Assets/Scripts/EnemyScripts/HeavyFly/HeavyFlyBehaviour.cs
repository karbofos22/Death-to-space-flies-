using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyFlyBehaviour : MonoBehaviour, IEnemy
{
    #region Fields
    [SerializeField] private ParticleSystem bloodParticle;
    private readonly int emissionParticleToEmit = 240;
    private bool gotPoints;
    [SerializeField] private int hp = 850;
    private readonly int hpLowerLimit = 100;
    [SerializeField] private int scoreValue = 200;
    #endregion

    #region Properties
    public int Hp { get; private set; }
    public int ScoreValue { get; private set; }
    #endregion

    void Start()
    {
        Hp = hp;
        ScoreValue = scoreValue;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<LaserProjectile>())
        {
            TakeDamage(LaserProjectile.damageAmount);
        }
        if (other.GetComponent<Obstacle>())
        {
            TakeDamage(Obstacle.damageAmount);
            Destroy(other.gameObject);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<BeamProjectile>())
        {
            TakeDamage(BeamProjectile.damageAmount);
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
            Destroy(gameObject);
        }
        else
        {
            bloodParticle.Play();
        }
    }
}

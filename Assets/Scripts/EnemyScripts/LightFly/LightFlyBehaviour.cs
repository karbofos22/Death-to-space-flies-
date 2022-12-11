using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LightFlyBehaviour : MonoBehaviour, IEnemy
{
    #region Fields
    public ParticleSystem bloodParticle;
    private bool gotPoints;
    #endregion

    #region Properties
    public int Hp { get; set; }
    public int ScoreValue { get; set; }
    #endregion

    void Start()
    {
        Hp = 200;
        ScoreValue = 100;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            TakeDamage(LaserProjectile.damageAmount);
        }
        if (other.CompareTag("Obstacle"))
        {
            TakeDamage(Obstacle.damageAmount);
            Destroy(other.gameObject);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Beam"))
        {
            TakeDamage(BeamProjectile.damageAmount);
        }
    }
    public void TakeDamage(int amount)
    {
        Hp -= amount;
        if (Hp < 50)
        {
            bloodParticle.Emit(60);
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

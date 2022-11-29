using System.Collections;
using UnityEngine;
public class PlayerBehaviour : MonoBehaviour
{
    public int hp;
    public bool isAlive;

    private Material whiteMat;
    private Material defaultMat;
    MeshRenderer meshRenderer;

    private GameManager gameManager;
    public ParticleSystem explosion;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        meshRenderer = GetComponent<MeshRenderer>();
        whiteMat = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        defaultMat = meshRenderer.material;
        hp = 1300;
        isAlive = true;

        StartCoroutine(Death());
    }

    #region Methods
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
            TakeDamage(2000);
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
            isAlive = false;
        }
        else
        {
            Invoke(nameof(ResetMat), .13f);
        }
    }
    IEnumerator Death()
    {
        yield return new WaitUntil(() => hp <= 0);


        yield return new WaitForSeconds(3.6f);

        explosion.Stop();
        Destroy(gameObject);
        gameManager.isGameActive = false;
        gameManager.isGameOver = true;
        
    }
    public void HpRestore(int hpAmount)
    {
        hp += hpAmount;
        if (hp > 1000)
        {
            hp = 1000;
        }
    }
    void ResetMat()
    {
        meshRenderer.material = defaultMat;
    }
    #endregion
}

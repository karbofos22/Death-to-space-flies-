using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public int hp = 500;
    public const int damageAmount = 45;

    readonly float rotateSpeed = 400f;
    private float speed;
    private Rigidbody obstacleRb;
    private Vector3 rotation;

    private Material whiteMat;
    private Material defaultMat;
    MeshRenderer meshRenderer;

    private SpawnManager spawnManager;
    private GameManager gameManager;
    
    private void Awake()
    {
        RandomSpeed();
        RandomRotation();
    }
    void Start()
    {
        obstacleRb = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();

        whiteMat = Resources.Load("WhiteFlash", typeof(Material)) as Material;

        defaultMat = meshRenderer.material;

        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    void FixedUpdate()
    {
        Behaviour();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            TakeDamage(LaserProjectile.damageAmount);
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
    void RandomSpeed()
    {
        speed = Random.Range(10, 21);
    }
    void RandomRotation()
    {
        Vector3[] rotationDir = new Vector3[] { Vector3.up, Vector3.forward, Vector3.right, Vector3.left, Vector3.back, Vector3.down };
        rotation = rotationDir[Random.Range(0, rotationDir.Length)];
    }
    void Behaviour()
    {
        obstacleRb.AddTorque(rotation * rotateSpeed);
        obstacleRb.AddForce(Vector3.back * speed);
        if (obstacleRb.velocity.magnitude > speed)
        {
            obstacleRb.velocity = obstacleRb.velocity.normalized * speed;
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

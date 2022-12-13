using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    #region Fields
    [SerializeField] private int hp = 500;
    public const int damageAmount = 45;

    private readonly float rotateSpeed = 400f;
    private float speed;
    private Rigidbody obstacleRb;
    private Vector3 rotation;

    [SerializeField] private Material whiteMat;
    private Material defaultMat;
    private MeshRenderer meshRenderer;
    #endregion

    private void Start()
    {
        obstacleRb = GetComponent<Rigidbody>();

        meshRenderer = GetComponent<MeshRenderer>();
        defaultMat = meshRenderer.material;

        RandomSpeed();
        RandomRotation();
    }
    private void FixedUpdate()
    {
        Behaviour();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<LaserProjectile>())
        {
            TakeDamage(LaserProjectile.damageAmount);
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
    private void RandomSpeed()
    {
        speed = Random.Range(10, 21);
    }
    private void RandomRotation()
    {
        Vector3[] rotationDir = new Vector3[] { Vector3.up, Vector3.forward, Vector3.right, Vector3.left, Vector3.back, Vector3.down };
        rotation = rotationDir[Random.Range(0, rotationDir.Length)];
    }
    private void Behaviour()
    {
        obstacleRb.AddTorque(rotation * rotateSpeed);
        obstacleRb.AddForce(Vector3.back * speed);
        if (obstacleRb.velocity.magnitude > speed)
        {
            obstacleRb.velocity = obstacleRb.velocity.normalized * speed;
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

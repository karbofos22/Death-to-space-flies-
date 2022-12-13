using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    #region Fields
    public const int damageAmount = 25;
    public static readonly float speed = 22f;

    private Rigidbody projectileRb;
    private Vector3 rotation;
    private readonly float rotateSpeed = 200f;
    #endregion

    private void Start()
    {
        projectileRb = GetComponent<Rigidbody>();
        RandomRotation();
    }

    private void FixedUpdate()
    {
        projectileRb.AddTorque(rotation * rotateSpeed);
        if (projectileRb.velocity.magnitude == 0)
        {
            Destroy(gameObject);
        }
    }
    private void RandomRotation()
    {
        Vector3[] rotationDir = new Vector3[] { Vector3.up, Vector3.forward, Vector3.right, Vector3.left, Vector3.back, Vector3.down };
        rotation = rotationDir[Random.Range(0, rotationDir.Length)];
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Beam"))
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Beam"))
        {
            Destroy(gameObject);
        }
    }
}

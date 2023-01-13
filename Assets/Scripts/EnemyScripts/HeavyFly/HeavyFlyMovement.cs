using UnityEngine;

public class HeavyFlyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    private Vector3 destroyerPos = new(-1.8452f, 0, -134.2f);

    private void Update()
    {
        Movement();
    }
    private void Movement()
    {
        transform.position = Vector3.MoveTowards(transform.position, destroyerPos, moveSpeed * Time.deltaTime);
    }
}

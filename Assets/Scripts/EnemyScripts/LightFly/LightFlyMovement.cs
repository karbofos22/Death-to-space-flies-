using UnityEngine;

public class LightFlyMovement : MonoBehaviour
{
    #region Fields
    [SerializeField] private float moveSpeed = 5.5f;
    private Vector3 startPosX;
    [SerializeField] private float strafeSpeed = 4;
    [SerializeField] private float strafeDelta = 3.5f;

    private Vector3 destroyerPos = new(-1.8452f, 0, -134.2f);
    #endregion

    void Start()
    {
        startPosX = transform.position;
    }
    void Update()
    {
        Movement();
    }
    void Movement()
    {
        transform.position = Vector3.MoveTowards(StrafeByX(), destroyerPos, moveSpeed * Time.deltaTime);
    }
    Vector3 StrafeByX()
    {
        Vector3 v = startPosX;
        v.x += strafeDelta * Mathf.Sin(Time.time * strafeSpeed);
        v.y = startPosX.y;
        v.z = transform.position.z;

        return v;
    }
}

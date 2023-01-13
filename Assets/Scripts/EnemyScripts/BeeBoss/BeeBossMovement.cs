using UnityEngine;

public class BeeBossMovement : MonoBehaviour
{
    #region Fields
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float strafeSpeed = 3f;
    [SerializeField] private float strafeDelta = 12.4f;
    private readonly float eps = 0.05f;
    private Vector3 stopPoint = new(0, 0, 43.5f);
    public bool isCanStrafe;
    #endregion

    private void Update()
    {
        Movement();
    }
    #region Methods
    private void Movement()
    {
        MoveToPos();

        if (isCanStrafe)
        {
            transform.position = StrafeByX();
        }
    }
    private Vector3 StrafeByX()
    {
        Vector3 strafeVector = stopPoint;
        strafeVector.x += strafeDelta * Mathf.Sin(Time.time * strafeSpeed);
        strafeVector.y = stopPoint.y;
        strafeVector.z = transform.position.z;

        return strafeVector;
    }
    private void MoveToPos()
    {
        transform.position = Vector3.MoveTowards(transform.position, stopPoint, moveSpeed * Time.deltaTime);
        if ((transform.position - stopPoint).magnitude <= eps)
        {
            isCanStrafe = true;
            GlobalEventManager.SendBossReadyToFight();
        }
    }
    #endregion
}


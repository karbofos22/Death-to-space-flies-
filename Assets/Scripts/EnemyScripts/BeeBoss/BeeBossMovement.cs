using UnityEngine;

public class BeeBossMovement : MonoBehaviour
{
    #region Fields
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float strafeSpeed = 3f;
    [SerializeField] private float strafeDelta = 12.4f;
    private Vector3 stopPoint = new(0, 0, 43.5f);
    private bool isCanStrafe;
    #endregion

    void Update()
    {
        Movement();
    }
    #region Methods
    void Movement()
    {
        MoveToPos();

        if (isCanStrafe)
        {
            transform.position = StrafeByX();
        }
    }
    Vector3 StrafeByX()
    {
        Vector3 v = stopPoint;
        v.x += strafeDelta * Mathf.Sin(Time.time * strafeSpeed);
        v.y = stopPoint.y;
        v.z = transform.position.z;

        return v;
    }
    void MoveToPos()
    {
        transform.position = Vector3.MoveTowards(transform.position, stopPoint, moveSpeed * Time.deltaTime);
        if (transform.position == stopPoint)
        {
            isCanStrafe = true;
            GlobalEventManager.SendBossFight();
        }
    }
    #endregion
}


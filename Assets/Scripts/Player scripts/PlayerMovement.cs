using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region
    private Rigidbody playerRb;
    [SerializeField] private float steeringMod = 200f;

    private readonly int border = 42;
    #endregion

    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        Movement();
    }
    private void Movement()
    {
        playerRb.velocity = new Vector3(Input.GetAxis("Horizontal") * steeringMod, 0);

        if (transform.position.x < -border)
        {
            transform.position = new Vector3(-border, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > border)
        {
            transform.position = new Vector3(border, transform.position.y, transform.position.z);
        }
    }
}

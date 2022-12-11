using UnityEngine;

public class SkyBoxCamRotate : MonoBehaviour
{
    private readonly float rotateSpeed = -0.006f;
    void Update()
    {
        transform.Rotate(rotateSpeed, 0, 0);
    }
}

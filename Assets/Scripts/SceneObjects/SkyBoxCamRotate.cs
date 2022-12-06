using UnityEngine;

public class SkyBoxCamRotate : MonoBehaviour
{
        public const string Horizontal = "Horizontal";

    void Update()
    {
        float speed = Input.GetAxisRaw(Horizontal) * 1f;
        transform.Rotate(-0.006f, 0, 0);
    }
}

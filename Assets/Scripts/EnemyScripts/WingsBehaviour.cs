using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingsBehaviour : MonoBehaviour
{
    readonly float delta = 0.41f;
    readonly float speed = 46f;

    private Quaternion startPos;

    void Start()
    {
        startPos = transform.localRotation;
    }
    void Update()
    {
        Quaternion rotation = startPos;

        rotation.z += delta * Mathf.Sin(Time.time * speed);
        transform.localRotation = rotation;
    }
}

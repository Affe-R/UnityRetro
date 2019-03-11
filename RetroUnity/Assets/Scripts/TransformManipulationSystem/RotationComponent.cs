using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationComponent : MonoBehaviour
{
    public float speed = 1;

    void Update()
    {
        transform.RotateAround(transform.position, Vector3.forward, Time.deltaTime * speed);
    }
}

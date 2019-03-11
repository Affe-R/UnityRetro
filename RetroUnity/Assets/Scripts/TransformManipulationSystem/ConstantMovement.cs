using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantMovement : MonoBehaviour
{
    public float speed = 1;
    public Vector2 direction = Vector2.down;

    void Update()
    {
        transform.Translate(direction * speed, Space.World);
    }
}

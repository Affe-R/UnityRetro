using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveComponent : MonoBehaviour
{
    [Range(0.01f, 5.0f)]
    public float moveSpeed = 1;

    [Range(0.01f, 5.0f)]
    public float jumpForce = 1;

    public LayerMask jumpAreas;

    Rigidbody2D rigidbody;

    public void Move(float horizontal)
    {
        Vector2 moveDir = new Vector2(horizontal, 0);
        Move(moveDir);
    }

    public void Move(Vector2 moveDir)
    {
        if(moveDir.magnitude > 1)
            moveDir = moveDir.normalized;

        moveDir *= moveSpeed;
        moveDir.y = rigidbody.velocity.y;
        rigidbody.velocity = moveDir;
    }

    public void Jump()
    {
        if(IsGrounded())
            rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    public bool IsGrounded()
    {
        SpriteRenderer rend = GetComponent<SpriteRenderer>();

        Vector2 origin = (Vector2)rend.bounds.center - new Vector2(0, rend.bounds.size.y / 2);
        Debug.Log(origin);
        Vector2 size = Vector2.one * 0.01f;
        float angle = 0;
        Vector2 direction = Vector2.down;
        float distance = 1.0f;
        
        RaycastHit2D castHit = Physics2D.BoxCast(origin, size, angle, direction, distance, jumpAreas);
        Debug.Log(castHit.collider);
        return castHit.collider != null;
    }

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
}

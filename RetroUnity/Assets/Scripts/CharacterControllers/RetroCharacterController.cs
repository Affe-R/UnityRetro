using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class RetroCharacterController : MonoBehaviour
{
    public LayerMask groundMask;
    Rigidbody2D rb2d;
    CharacterController characterController;
    float moveHorizontal;
    float moveVertical;
    Vector2 velocity;

    [Header("Character Movement")]
    public float movementSpeed = 3f;
    public float jumpForce = 5f;


    [Header("Input")]
    public string horizontalAxis = "Horizontal";
    public string verticalAxis = "Vertical";
    public string jumpButton = "Jump";

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CharacterMovement();
        
    }
    private void FixedUpdate()
    {
        Move();
    }

    void CharacterMovement()
    {
        if (Input.GetButtonDown("Jump")){
            Jump();
        }
        Debug.DrawRay(transform.position, Vector2.down*0.5f, Color.red);

        moveHorizontal = Input.GetAxis(horizontalAxis);
        moveVertical = Input.GetAxis(verticalAxis);

        velocity = new Vector2(moveHorizontal, rb2d.velocity.y);
        ////transform.Translate(moveSum * movementSpeed * Time.deltaTime, Space.World);
    }

    void Move()
    {
        rb2d.velocity = (velocity * movementSpeed * Time.deltaTime);
    }

    void Jump()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down,0.5f,groundMask);
        if (hit.collider)
        {
            Debug.Log("Jumped");
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
            //rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        } else
        {
            Debug.Log("Jump unsuccessful");
        }
    }
}

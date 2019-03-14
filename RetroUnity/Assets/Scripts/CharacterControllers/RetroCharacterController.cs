using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class RetroCharacterController : MonoBehaviour
{
    public LayerMask groundMask;
    public LayerMask interactMask;

    Rigidbody2D rb2d;
    BoxCollider2D boxCollider2d;
    CharacterController characterController;
    float moveHorizontal;
    float moveVertical;
    Vector2 characterBounds;
    bool jump = false;
    Vector2 velocity;

    [Header("Character Movement")]
    public float movementSpeed = 10;
    public float jumpForce = 13f;


    [Header("Input")]
    public string horizontalAxis = "Horizontal";
    public string verticalAxis = "Vertical";
    public string jumpButton = "Jump";
    public string interactButton = "Interact";

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        characterBounds = GetComponent<BoxCollider2D>().size;
    }

    private void Update()
    {
        // Debug.Log(rb2d.velocity);
        CharacterMovement();
        CharacterInteract();
    }
    private void FixedUpdate()
    {
        Move();
    }

    void CharacterMovement()
    {
        if (Input.GetButtonDown(jumpButton)){
            jump = true;
        }
        Debug.DrawRay(transform.position, Vector2.down*0.5f, Color.red);

        moveHorizontal = Input.GetAxis(horizontalAxis);
        moveVertical = Input.GetAxis(verticalAxis);

        velocity = new Vector2(moveHorizontal, 0);
        
        ////transform.Translate(moveSum * movementSpeed * Time.deltaTime, Space.World);
    }
    #region CharacterMovement;
    void Move()
    {
        Vector2 velocity = this.velocity;
        velocity *= movementSpeed;
        velocity.y = rb2d.velocity.y;

        rb2d.velocity = (velocity);
        //Debug.Log(velocity * movementSpeed * Time.deltaTime);
        if (jump)
        {
            Debug.Log("ShouldJump");
            Jump();
            jump = false;
        }
    }

    void Jump()
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position,characterBounds, 0f, Vector2.down,0.5f,groundMask);
        
        if (hit.collider)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
            //rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        } else
        {
            Debug.Log("Jump unsuccessful");
        }
    }
    #endregion CharacterMove

    void CharacterInteract()
    {
        // if (Input.GetButtonDown(interactButton)){
        //     Collider2D[] hitColliders = Physics2D.OverlapBoxAll(transform.position, characterBounds, 0f, interactMask);
        //     foreach(Collider2D collider in hitColliders)
        //     {
        //         collider.gameObject.GetComponent<ButtonTrigger>();
        //     }
        // }
    }
}

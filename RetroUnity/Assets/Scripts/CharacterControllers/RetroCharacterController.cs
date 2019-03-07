using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class RetroCharacterController : MonoBehaviour
{

    Rigidbody2D rigidBody;
    float moveHorizontal;
    float moveVertical;

    [Header("Character Movement")]
    public float movementSpeed = 3f;
    public float jumpForce = 5f;

    [Header("Input")]
    public string horizontalAxis = "Horizontal";
    public string verticalAxis = "Vertical";
    public string jumpButton = "Jump";

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        CharacterMovement();
        
    }

    void CharacterMovement()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            Jump();
        }

        moveHorizontal = Input.GetAxis(horizontalAxis);
        moveVertical = Input.GetAxis(verticalAxis);

        Vector3 moveSum = new Vector3(moveHorizontal, 0, 0);

        transform.Translate(moveSum * movementSpeed * Time.deltaTime, Space.World);
    }

    void Jump()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down,1f);
        if (hit.collider)
        {
            rigidBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        } else
        {
            Debug.Log("Jump unsuccessful");
        }
    }
}

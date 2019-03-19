using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [Header("Control Scheme")]
    public string jumpInput = "Jump";
    public string horizontalInput = "Horizontal";
    public string verticalInput = "Vertical";

    [Header("Character physical properties")]
    public float moveSpeed = 5f;
    public float groundAcceleration = .1f;
    public float airAcceleration = .2f;
    [Tooltip("Jump height in world units")]
    public float jumpHeight = 5f;
    [Tooltip("Time it takes to reach jump peak")]
    public float jumpTimeApex = .4f;
    [Tooltip("The steepest angle the player can traverse")]
    [Range(0f,90f)]
    public float maxWalkAngle = 30f;
    [Tooltip("The steepest angle the player can descend")]
    [Range(0f, 90f)]
    public float maxDescentAngle = 30f;

    [Header("Character visual properties")]
    [Tooltip("Is the default orientation of the sprite facing right?")]
    public bool defaultDirectionRight;
    public Color characterTint = Color.white;
    float gravity;
    float jumpVelocity;

    float velocityXSmoothing;
    Vector3 velocity;
    Vector2 directionalInput;
    bool wantsToJump;

    Controller2D controller;
    Animator animator;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        gravity = -(jumpHeight * 2) / (jumpTimeApex * jumpTimeApex);
        jumpVelocity = Mathf.Abs(gravity) * jumpTimeApex;
        controller = GetComponent<Controller2D>();
        controller.maxWalkableAngle = maxWalkAngle;
        controller.maxDescendableAngle = maxDescentAngle;
        print(gravity);
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = characterTint;
    }

    private void Update()
    {
        directionalInput = new Vector2(Input.GetAxis(horizontalInput), Input.GetAxis(verticalInput));
        if (Input.GetButtonDown(jumpInput) && controller.collisionInfo.below)
        {
            wantsToJump = true;
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(controller.collisionInfo.above || controller.collisionInfo.below)
        {
            velocity.y = 0;
        }

        float targetVelocityX = directionalInput.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisionInfo.below)?groundAcceleration:airAcceleration);
        velocity.y += gravity * Time.deltaTime;
        if (wantsToJump)
        {
            velocity.y = jumpVelocity;
            animator.SetBool("IsJumping", true);
            wantsToJump = false;
        }
        else if (controller.collisionInfo.below)
        {
            animator.SetBool("IsJumping", false);
        }

        controller.Move(velocity * Time.deltaTime);
        animator.SetBool("isGrounded", controller.collisionInfo.below);
        animator.SetFloat("Speed", Mathf.Abs(velocity.x));
        if (velocity.x > 0)
        {
            spriteRenderer.flipX = defaultDirectionRight;
        }
        else if (velocity.x < 0)
        {
            spriteRenderer.flipX = !defaultDirectionRight;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
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

    float gravity;
    float jumpVelocity;

    float velocityXSmoothing;
    Vector3 velocity;
    Vector2 directionalInput;
    bool wantsToJump;

    Controller2D controller;


    // Start is called before the first frame update
    void Start()
    {
        gravity = -(jumpHeight * 2) / (jumpTimeApex * jumpTimeApex);
        jumpVelocity = Mathf.Abs(gravity) * jumpTimeApex;
        controller = GetComponent<Controller2D>();
        controller.maxWalkableAngle = maxWalkAngle;
        controller.maxDescendableAngle = maxDescentAngle;
        print(gravity);

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
            wantsToJump = false;
        }

        controller.Move(velocity * Time.deltaTime);

    }
}

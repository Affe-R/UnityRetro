using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Controller2D : MonoBehaviour
{
    public LayerMask collisionMask;

    BoxCollider2D collider2d;
    RaycastOrigins raycastOrigins;
    public CollisionInfo collisionInfo;

    public float maxWalkableAngle;
    public float maxDescendableAngle;

    const float skinWidth = 0.01f;
    public int rayCountHorizontal;
    public int rayCountVertical;

    float raySpacingHorizontal;
    float raySpacingVertical;


    private void Start()
    {
        collider2d = GetComponent<BoxCollider2D>();
        CalculateRaySpacing();
    }


    public void Move(Vector3 _velocity)
    {
        UpdateRaycastOrigins();
        collisionInfo.Reset();
        collisionInfo.velocityOld = _velocity;
        if (_velocity.y < 0)
        {
            DescendSlope(ref _velocity);
        }
        if (_velocity.x != 0)
        {
            HorizontalCollision(ref _velocity);
        }
        if (_velocity.y != 0)
        {
            VerticalCollision(ref _velocity);

        }
        transform.Translate(_velocity);
    }

    #region CollisionHandling

    void HorizontalCollision(ref Vector3 _velocity)
    {

        float directionX = Mathf.Sign(_velocity.x);
        float rayLenght = Mathf.Abs(_velocity.x) + skinWidth;

        for (int i = 0; i < rayCountHorizontal; i++)
        {
            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
            rayOrigin += Vector2.up * (raySpacingHorizontal * i);
            RaycastHit2D hit = (Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLenght, collisionMask));
            if (hit)
            {
                float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
                if (i == 0 && slopeAngle <= maxWalkableAngle)
                {
                    if (collisionInfo.descendingSlope)
                    {
                        collisionInfo.descendingSlope = false;
                        _velocity = collisionInfo.velocityOld;
                    }
                    float distanceToSlopeStart = 0;
                    if (slopeAngle != collisionInfo.slopeAngleOld)
                    {

                        distanceToSlopeStart = hit.distance - skinWidth;
                        _velocity.x -= distanceToSlopeStart * directionX;
                    }
                    ClimbSlope(ref _velocity, slopeAngle);
                    _velocity.x += distanceToSlopeStart * directionX;
                }

                if (!collisionInfo.climbingSlope || slopeAngle > maxWalkableAngle)
                {

                    _velocity.x = (hit.distance - skinWidth) * directionX;
                    rayLenght = hit.distance;

                    if (collisionInfo.climbingSlope)
                    {
                        _velocity.y = Mathf.Tan(collisionInfo.slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(_velocity.x);
                    }

                    collisionInfo.left = directionX == -1;
                    collisionInfo.right = directionX == 1;
                }
            }

            Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLenght, Color.green);


        }
    }

    void VerticalCollision(ref Vector3 _velocity)
    {

        float directionY = Mathf.Sign(_velocity.y);
        float rayLenght = Mathf.Abs(_velocity.y) + skinWidth;

        for (int i = 0; i < rayCountVertical; i++)
        {
            Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
            rayOrigin += Vector2.right * (raySpacingVertical * i + _velocity.x);
            RaycastHit2D hit = (Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLenght, collisionMask));
            if (hit)
            {
                _velocity.y = (hit.distance - skinWidth) * directionY;
                rayLenght = hit.distance;
                Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLenght, Color.green);

                if (collisionInfo.climbingSlope)
                {
                    _velocity.x = _velocity.y / Mathf.Tan(collisionInfo.slopeAngle * Mathf.Deg2Rad) * Mathf.Sign(_velocity.x);
                }

                collisionInfo.below = directionY == -1;
                collisionInfo.above = directionY == 1;
            }
            
        }

        if (collisionInfo.climbingSlope)
        {
            float directionX = Mathf.Sign(_velocity.x);
            rayLenght = Mathf.Abs(_velocity.x) + skinWidth;
            Vector2 rayOrigin = ((directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight) + Vector2.up * _velocity.y;
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLenght, collisionMask);

            if (hit)
            {
                float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
                if (slopeAngle != collisionInfo.slopeAngle)
                {
                    _velocity.x = (hit.distance - skinWidth) * directionX;
                }
            }
        }
    }

    void ClimbSlope(ref Vector3 _velocity, float _slopeAngle)
    {
        float moveDistance = Mathf.Abs(_velocity.x);

        float climbVelocityY = moveDistance * Mathf.Sin(_slopeAngle * Mathf.Deg2Rad);
        if (_velocity.y <= climbVelocityY)
        {
            _velocity.y = climbVelocityY;
            _velocity.x = moveDistance * Mathf.Cos(_slopeAngle * Mathf.Deg2Rad) * Mathf.Sign(_velocity.x);
            collisionInfo.below = true;
            collisionInfo.climbingSlope = true;
            collisionInfo.slopeAngle = _slopeAngle;
        }

    }

    void DescendSlope (ref Vector3 _velocity)
    {
        float directionX = Mathf.Sign(_velocity.x);
        Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomRight : raycastOrigins.bottomLeft;
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, -Vector2.up, Mathf.Infinity, collisionMask);
        if (hit)
        {
            float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
            if(slopeAngle != 0 && slopeAngle <= maxDescendableAngle)
            {
                if (Mathf.Sign(hit.normal.x) == directionX)
                {
                    if (hit.distance - skinWidth <= Mathf.Tan(slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(_velocity.x))
                    {
                        float moveDistance = Mathf.Abs(_velocity.x);
                        float descendVelocityY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;
                        _velocity.x = moveDistance * Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * Mathf.Sign(_velocity.x);
                        _velocity.y -= descendVelocityY;

                        collisionInfo.slopeAngle = slopeAngle;
                        collisionInfo.descendingSlope = true;
                        collisionInfo.below = true;
                    }
                }
            }
        }
    }

    public struct CollisionInfo
    {
        public bool above, below;
        public bool left, right;

        public bool climbingSlope, descendingSlope;
        public float slopeAngle, slopeAngleOld;

        public Vector3 velocityOld;

        public void Reset()
        {
            above = false;
            below = false;
            left = false;
            right = false;
            climbingSlope = false;
            descendingSlope = false;
            slopeAngleOld = slopeAngle;
            slopeAngle = 0f;
        }
        public string PrintResult()
        {
            string returnString = "above: " + above + " below: " + below + " left: " + left + " right: " + right;
            return returnString;
        }
    }
    #endregion CollisionHandling

    #region Raycast Setup
    struct RaycastOrigins
    {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }
    void UpdateRaycastOrigins()
    {
        Bounds bounds = collider2d.bounds;
        bounds.Expand(skinWidth * -2);

        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    void CalculateRaySpacing()
    {
        Bounds bounds = collider2d.bounds;
        bounds.Expand(skinWidth * -2);

        rayCountHorizontal = Mathf.Clamp(rayCountHorizontal, 2, int.MaxValue);
        rayCountVertical = Mathf.Clamp(rayCountVertical, 2, int.MaxValue);

        raySpacingHorizontal = bounds.size.y / (rayCountHorizontal - 1);
        raySpacingVertical = bounds.size.x / (rayCountVertical - 1);

    }
    #endregion Raycast Setup
}

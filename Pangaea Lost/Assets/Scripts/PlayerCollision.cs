using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField]
    PlayerScript playerScript;

    const float skinWidth = .015f; //inset for rays to shoot from on the player
    public int horizontalRayCount = 4; //number of horizontal rays. Can be changed in editor and ray amounts and spacing will update
    public int verticalRayCount = 4; //number of vertical rays. Can be changed in editor and ray amounts and spacing will update

    float horizontalRaySpacing; //variable to hold spacing for horizontal rays
    float verticalRaySpacing; //variable to hold spacing for vertical rays

    float maxClimbAngle = 80; //Maximum angle that player can climb/move
    float maxDescendAngle = 80; //Maximum angle that player can descend/move

    CapsuleCollider collider; //variable for capsule collider attached to player
    RaycastOrigins raycastOrigins; // variable for raycasting origins. Struct is at the bottom that holds the raycasting positions.

    public LayerMask collisionMask; //variable that determines what layer(s) will collide with player
    public CollisionInfo collisions; //variable to determine where collision happened

    // Start is called before the first frame update
    void Start()
    {
        print("PlayerCollision Starting");
        collider = GetComponent<CapsuleCollider>();

        CalculateRaySpacing();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Function detects horizontal collisions. Shoots/detects collision based on the direction.
    public void HorizontalCollisions(ref Vector3 velocity)
    {
        float directionZ = Mathf.Sign(velocity.z);
        float rayLength = Mathf.Abs(velocity.z) + skinWidth;

        for (int i = 0; i < horizontalRayCount; i++)
        {
            //Vector3 rayOrigin = (directionZ == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
            //rayOrigin += Vector3.up * (horizontalRaySpacing * i);
            Vector3 rayOrigin = Vector3.zero;

            Vector3 rayDirection = Vector3.zero;

            if (playerScript.playerMovement.facingRight)
            {
                rayDirection = Vector3.right;
                rayOrigin = raycastOrigins.bottomRight;
            }
            else if (!playerScript.playerMovement.facingRight)
            {
                rayDirection = Vector3.left;
                rayOrigin = raycastOrigins.bottomLeft;
            }

            rayOrigin += Vector3.up * (horizontalRaySpacing * i);

            RaycastHit hit;

            Debug.DrawRay(rayOrigin, rayDirection * directionZ * rayLength * 40, Color.yellow);

            if (Physics.Raycast(rayOrigin, rayDirection * directionZ, out hit, rayLength, collisionMask))
            {
                float slopeAngle = Vector3.Angle(hit.normal, Vector2.up);

                if (i == 0 && slopeAngle <= maxClimbAngle)
                {
                    if (collisions.descendingSlope)
                    {
                        collisions.descendingSlope = false;
                        velocity = collisions.velocityOld;
                    }

                    float distanceToSlopeStart = 0;

                    if (slopeAngle != collisions.slopeAngleOld)
                    {
                        distanceToSlopeStart = hit.distance - skinWidth;
                        velocity.z -= distanceToSlopeStart * directionZ;
                    }

                    ClimbSlope(ref velocity, slopeAngle);
                    velocity.z += distanceToSlopeStart * directionZ;
                }

                if (!collisions.climbingSlope || slopeAngle > maxClimbAngle)
                {
                    velocity.z = (hit.distance - skinWidth) * directionZ;
                    rayLength = hit.distance;

                    if (collisions.climbingSlope)
                    {
                        velocity.y = Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(velocity.x);
                    }

                    collisions.left = directionZ == -1;
                    collisions.right = directionZ == 1;
                }
            }
        }
    }

    //Function detects vertical collisions. Shoots/detects collision based on the direction.
    public void VerticalCollisions(ref Vector3 velocity)
    {
        Debug.Log("Inside Vertical Collision");
        float directionY = Mathf.Sign(velocity.y);
        float rayLength = Mathf.Abs(velocity.y) + skinWidth;

        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector3 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
            rayOrigin += Vector3.right * (verticalRaySpacing * i + velocity.z);
            RaycastHit hit;

            Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.yellow);

            if (Physics.Raycast(rayOrigin, Vector2.up * directionY, out hit, rayLength, collisionMask))
            {
                velocity.y = (hit.distance - skinWidth) * directionY;
                rayLength = hit.distance;

                if (collisions.climbingSlope)
                {
                    velocity.y = velocity.y / Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Sign(velocity.y);
                }

                collisions.below = directionY == -1;
                collisions.above = directionY == 1;
            }
        }

        if (collisions.climbingSlope)
        {
            float directionZ = Mathf.Sign(velocity.z);
            rayLength = Mathf.Abs(velocity.z) + skinWidth;
            Vector3 rayOrigin = ((directionZ == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight) + Vector3.up * velocity.y;
            RaycastHit hit;

            if (Physics.Raycast(rayOrigin, Vector3.right * directionZ, out hit, rayLength, collisionMask))
            {
                float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
                if (slopeAngle != collisions.slopeAngle)
                {
                    velocity.z = (hit.distance - skinWidth) * directionZ;
                    collisions.slopeAngle = slopeAngle;
                }
            }
        }
    }

    public void ClimbSlope(ref Vector3 velocity, float slopeAngle)
    {
        float moveDistance = Mathf.Abs(velocity.z);
        float climbVelocityY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;

        if (velocity.y <= climbVelocityY)
        {
            velocity.y = climbVelocityY;
            velocity.z = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(velocity.z);
            collisions.below = true;
            collisions.climbingSlope = true;
            collisions.slopeAngle = slopeAngle;
        }
    }

    public void DescendSlope(ref Vector3 velocity)
    {
        float directionZ = Mathf.Sign(velocity.z);
        Vector3 rayOrigin = (directionZ == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
        RaycastHit hit;

        if (Physics.Raycast(rayOrigin, -Vector2.up, out hit, Mathf.Infinity, collisionMask))
        {
            float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
            if (slopeAngle != 0 && slopeAngle <= maxDescendAngle)
            {
                if (Mathf.Sign(hit.normal.z) == directionZ)
                {
                    if (hit.distance - skinWidth <= Mathf.Tan(slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(velocity.z))
                    {
                        float moveDistance = Mathf.Abs(velocity.z);
                        float descendVelocityY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;
                        velocity.z = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(velocity.z);
                        velocity.y -= descendVelocityY;

                        collisions.slopeAngle = slopeAngle;
                        collisions.descendingSlope = true;
                        collisions.below = true;
                    }
                }
            }
        }
    }

    public void UpdateRaycastOrigins()
    {
        Debug.Log("Inside UpdateRaycastOrigins");
        Bounds bounds = collider.bounds;
        bounds.Expand(skinWidth * -2);

        raycastOrigins.bottomLeft = new Vector3(bounds.min.x, bounds.min.y, bounds.center.z);
        raycastOrigins.bottomRight = new Vector3(bounds.max.x, bounds.min.y, bounds.center.z);
        raycastOrigins.topLeft = new Vector3(bounds.min.x, bounds.max.y, bounds.center.z);
        raycastOrigins.topRight = new Vector3(bounds.max.x, bounds.max.y, bounds.center.z);
    }

    public void CalculateRaySpacing()
    {
        Debug.Log("Inside CalculateRaySpacing");
        Bounds bounds = collider.bounds;
        bounds.Expand(skinWidth * -2);

        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);

        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }

    public struct RaycastOrigins
    {
        public Vector3 topLeft, topRight;
        public Vector3 bottomLeft, bottomRight;
    }

    public struct CollisionInfo
    {
        public bool above, below;
        public bool left, right;

        public bool climbingSlope;
        public bool descendingSlope;
        public float slopeAngle, slopeAngleOld;
        public Vector3 velocityOld;

        public void Reset()
        {
            above = below = false;
            left = right = false;

            climbingSlope = false;
            descendingSlope = false;

            slopeAngleOld = slopeAngle;
            slopeAngle = 0;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField]
    PlayerScript playerScript;

    CapsuleCollider capCollider;

    int horizontalRayCount = 4;
    float horizontalRaySpacing;

    RaycastOrigins raycastOrigins;

    // Start is called before the first frame update
    void Start()
    {
        capCollider = GetComponent<CapsuleCollider>();

        CalculateRaySpacing();
    }

    public void HorizontalCollision(Vector3 input)
    {
        float directionX = Mathf.Sign(input.x);
        float rayLength = Mathf.Abs(input.x);

        for(int i = 0; i < horizontalRayCount; i++)
        {
            Vector3 rayOrigin = Vector3.zero;
            Vector3 rayDirection = Vector3.zero;

            if(playerScript.facingRight)
            {
                rayDirection = Vector3.right;
                rayOrigin = raycastOrigins.midRight;
            }
            else if(!playerScript.facingRight)
            {
                rayDirection = Vector3.left;
                rayOrigin = raycastOrigins.midLeft;
            }

            rayOrigin += Vector3.up * (horizontalRaySpacing * i);

            RaycastHit hit;

            Debug.DrawRay(rayOrigin, rayDirection * directionX * rayLength * 2, Color.red);

            if(Physics.Raycast(rayOrigin, rayDirection * directionX, out hit, rayLength))
            {
                float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
                print(slopeAngle);
            }

            print("Inside for loop of horizontal collision");
        }
    }

    void CalculateRaySpacing()
    {
        Bounds bounds = capCollider.bounds;

        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);

        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
    }

    public void UpdateRaycastOrigins()
    {
        Bounds bounds = capCollider.bounds;

        raycastOrigins.midRight = new Vector3(bounds.max.x, bounds.min.y, bounds.center.z);
        raycastOrigins.midLeft = new Vector3(bounds.min.x, bounds.min.y, bounds.center.z);
    }

    public struct RaycastOrigins
    {
        public Vector3 midLeft;
        public Vector3 midRight;
    }
}

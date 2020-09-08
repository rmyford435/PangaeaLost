using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public class ItemCollision : MonoBehaviour
{
    [SerializeField]
    PlayerScript playerScript;

    public LayerMask itemCollisionMask;

    public int horizontalRayCount = 4;

    float horizontalRaySpacing;

    CapsuleCollider collider;
    RaycastOrigins raycastOrigins;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<CapsuleCollider>();
        CalculateRaySpacing();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ItemCollisions(ref Vector3 velocity)
    {
        float directionX = Mathf.Sign(velocity.x);
        float rayLength = Mathf.Abs(velocity.x);
        //float rayLength = Mathf.Abs(velocity.x) + skinWidth;

        for (int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

            Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, Color.red);

            if (hit)
            {
                velocity.x = (hit.distance - skinWidth) * directionX;
                rayLength = hit.distance;
            }
        }
    }

    public void UpdateRaycastOrigins()
    {
        Bounds bounds = collider.bounds;
        //bounds.Expand(skinWidth * -2);

        raycastOrigins.front = new Vector2(bounds.min.x, bounds.min.y);
        //raycastOrigins.back = new Vector2(bounds.max.x, bounds.min.y);
        //raycastOrigins.top = new Vector2(bounds.min.x, bounds.max.y);
        //raycastOrigins.bottom = new Vector2(bounds.max.x, bounds.max.y);
    }

    void CalculateRaySpacing()
    {
        Bounds bounds = collider.bounds;
        //bounds.Expand(skinWidth * -2);

        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);

        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
    }

    struct RaycastOrigins
    {
        public Vector2 front, back;
        public Vector2 top, bottom;
    }
}*/

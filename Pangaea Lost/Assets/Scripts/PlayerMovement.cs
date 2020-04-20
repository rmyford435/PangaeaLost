using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    PlayerScript playerScript;


    [SerializeField]
    float ass = 0.0f;

    public bool facingRight = true;
    float leftRightFacing = 1;

    // Start is called before the first frame update
    void Start()
    {
        print("PlayerMovement Starting");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        

    }

    public void Move(Vector3 velocity)
    {
        Debug.Log("Inside Move");
        playerScript.playerCollision.UpdateRaycastOrigins();
        playerScript.playerCollision.collisions.Reset();

        playerScript.playerCollision.collisions.velocityOld = velocity;

        if (velocity.y < 0)
        {
            playerScript.playerCollision.DescendSlope(ref velocity);
        }

        if (velocity.z != 0)
        {
            if (velocity.z > 0)
            {
                velocity.z = velocity.z * 1;
            }
            else if (velocity.z < 0)
            {
                velocity.z = velocity.z * -1;
            }

            playerScript.playerCollision.HorizontalCollisions(ref velocity);
        }
        if (velocity.y != 0)
        {
            playerScript.playerCollision.VerticalCollisions(ref velocity);
        }

        transform.Translate(velocity);
    }

    public void Flip()
    {
        leftRightFacing = leftRightFacing * -1;
        facingRight = !facingRight;
        this.transform.rotation = Quaternion.LookRotation(new Vector3(leftRightFacing, 0f, 0f), Vector3.up);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///========================================
/// The Main Player Script
///========================================

public class PlayerScript : MonoBehaviour
{
    //Store a reference to all the sub player scripts
    [SerializeField]
    internal PlayerInput playerInput;

    [SerializeField]
    internal PlayerMovement playerMovement;

    [SerializeField]
    internal PlayerCollision playerCollision;

    float speed;
    float walkSpeed = 1.5f;
    float runSpeed = 5f;
    float accelerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .1f;

    const float SPRINT_SPEED = 5.0f;
    const float WALK_SPEED = 1.5f;

    float gravity;
    Vector3 velocity;
    float jumpVelocity;
    public float jumpHeight = 4;
    public float timeToJumpApex = .4f;

    public Vector3 input;

    // Start is called before the first frame update
    void Start()
    {
        print("PlayerScript Starting");

        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        input = new Vector3(0f, Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));

        speed = Mathf.Lerp(speed, WALK_SPEED, Time.deltaTime * 2f);

        velocity.z = input.z * speed;

        velocity.y += gravity * Time.fixedDeltaTime;

        if (playerCollision.collisions.above || playerCollision.collisions.below)
        {
            velocity.y = 0;
        }

        if (playerInput.isJumping && playerCollision.collisions.below)
        {
            velocity.y = jumpVelocity;
        }

        if (input.z < 0 && playerMovement.facingRight)
        {
            playerMovement.Flip();
        }

        if (input.z > 0 && !playerMovement.facingRight)
        {
            playerMovement.Flip();
        }


        Debug.Log("velocity.z = " + velocity.z);

        playerMovement.Move(velocity * Time.deltaTime);
    }
}
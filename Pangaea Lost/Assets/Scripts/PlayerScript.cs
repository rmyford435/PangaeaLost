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

    [SerializeField]
    internal PlayerAnimate playerAnimate;

    //[SerializeField]
    //internal ItemCollision itemCollision;

    //Walking/Running Variables
    float speed;
    float walkSpeed = 1.5f;
    float runSpeed = 5f;

    const float SPRINT_SPEED = 5.0f;
    const float WALK_SPEED = 1.5f;

    //Jumping Variables
    float gravity;
    private Vector3 velocity;
    float maxJumpVelocity;
    float minJumpVelocity;
    public float maxJumpHeight = 4;
    public float minJumpHeight = 1;
    public float timeToJumpApex = .4f;
    float accelerationTimeAirborne = .1f;
    float accelerationTimeGrounded = .01f;

    public Vector3 directionalInput;

    public Vector3 Velocity
    {
        get => velocity;
        //set => velocity = value;
    }

    float velocityZSmoothing;

    // Start is called before the first frame update
    void Start()
    { 
        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) + maxJumpVelocity);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CalculateVelocity();

        if (directionalInput.z < 0 && playerMovement.facingRight)
        {
            playerMovement.Flip();
        }

        if (directionalInput.z > 0 && !playerMovement.facingRight)
        {
            playerMovement.Flip();
        }

        //speed = Mathf.Lerp(speed, WALK_SPEED, Time.deltaTime * 2f);
        //velocity.z = input.z * speed;

        playerMovement.Move(velocity * Time.deltaTime);

        if (playerCollision.collisions.above || playerCollision.collisions.below)
        {
            velocity.y = 0;
        }
    }

    public void SetDirectionalInput(Vector3 input)
    {
        directionalInput = input;
    }

    public void OnJumpInputDown()
    {
        if (playerCollision.collisions.below)
        {
            velocity.y = maxJumpVelocity;
        }
    }

    public void OnJumpInputUp()
    {
        if (velocity.y > minJumpVelocity)
        {
            velocity.y = minJumpVelocity;
        }
    }

    void CalculateVelocity()
    {
        float targetVelocityZ = directionalInput.z * walkSpeed;
        velocity.z = Mathf.SmoothDamp(velocity.z, targetVelocityZ, ref velocityZSmoothing, (playerCollision.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
    }

}
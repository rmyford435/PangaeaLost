using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //Store a reference to all the sub player scripts
    [SerializeField]
    internal PlayerInput playerInput;

    [SerializeField]
    internal PlayerAnimate playerAnimate;

    CharacterController player;

    [SerializeField]
    float walkSpeed = 2;
    [SerializeField]
    float runSpeed = 5;
    [SerializeField]
    public float animationSpeedPercent;

    float velocityXSmoothing;
    float targetVelocityX;

    [SerializeField]
    Vector3 velocity;

    [SerializeField]
    Vector3 directionalInput;

    public float timeToJumpApex = .5f;
    float maxJumpVelocity;
    float minJumpVelocity;
    public float maxJumpHeight = 4;
    public float minJumpHeight = 1;
    float accelerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .1f;


    float gravity;
    float jumpVelocity;

    public bool facingRight = true;
    float leftRightFacing = 1;


    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<CharacterController>();

        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) + maxJumpVelocity);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        CalculateVelocity();

        print(animationSpeedPercent);

        if (directionalInput.x < 0 && facingRight)
        {
            Flip();
        }

        if (directionalInput.x > 0 && !facingRight)
        {
            Flip();
        }

        player.Move(velocity * Time.deltaTime);
    }

    public void SetDirectionalInput(Vector3 input)
    {
        directionalInput = input;
    }

    public void OnJumpInputDown()
    {
        if (player.isGrounded)
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
        targetVelocityX = ((playerAnimate.isRunning) ? runSpeed : walkSpeed) * directionalInput.x;  //calculates a target velocity for x to be used in the next line

        //Applies a movement modifier, which starts at velociyt.x and goes to the target velocity for x. If the player is ground it applies the first smooth time and the second if it's false.
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (player.isGrounded) ? accelerationTimeGrounded : accelerationTimeAirborne);  

        /*if(player.isGrounded)
        {
            velocity.y = 0;
        }*/

        animationSpeedPercent = ((playerAnimate.isRunning) ? 1 : .5f) * directionalInput.x;

        velocity.y += gravity * Time.deltaTime; //calculates velocity.y (basically applying gravity)
    }

    public void Flip()
    {
        leftRightFacing = leftRightFacing * -1;
        facingRight = !facingRight;
        this.transform.rotation = Quaternion.LookRotation(new Vector3(leftRightFacing, 0f, 0f), Vector3.up);
    }
}

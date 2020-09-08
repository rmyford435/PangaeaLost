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

    [SerializeField]
    internal PlayerCollision playerCollision;

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

    private float _yVelocity;

    [SerializeField]
    Vector3 directionalInput;

    private float maxJumpVelocity;
    private float minJumpVelocity;

    private float maxJumpHeight = 3f;
    private float minJumpHeight = 1f;
    private float timeToJumpApex = .4f;
    private float accelerationTimeAirborne = .1f;
    private float accelerationTimeGrounded = .01f;

    [SerializeField]
    float gravity;

    public bool facingRight = true;
    float leftRightFacing = 1;

    public Inventory inventory;

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
        playerCollision.UpdateRaycastOrigins();
        playerCollision.HorizontalCollision(directionalInput);

        //print(animationSpeedPercent);

        if (directionalInput.x < 0 && facingRight)
        {
            Flip();
        }

        if (directionalInput.x > 0 && !facingRight)
        {
            Flip();
        }

        /*if(Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }*/

        //player.Move(velocity * Time.deltaTime);
    }

    public void SetDirectionalInput(Vector3 input)
    {
        directionalInput = input;
    }

    public void OnJumpInputDown()
    {
        Debug.Log("Inside OnJumpInputDown");
        if (player.isGrounded)
        {
            Debug.Log("Jumping");
            _yVelocity = maxJumpVelocity;
            //velocity.y = maxJumpVelocity;
        }
    }

    public void OnJumpInputUp()
    {
        Debug.Log("Inside OnJumpInputUp");
        if (_yVelocity > minJumpHeight)
        {
            _yVelocity = minJumpVelocity;
            //velocity.y = minJumpVelocity;
        }
    }

    void CalculateVelocity()
    {
        targetVelocityX = ((playerAnimate.isRunning) ? runSpeed : walkSpeed) * directionalInput.x;  //calculates a target velocity for x to be used in the next line

        //Applies a movement modifier, which starts at velociyt.x and goes to the target velocity for x. If the player is ground it applies the first smooth time and the second if it's false.
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (player.isGrounded) ? accelerationTimeGrounded : accelerationTimeAirborne);

        //animationSpeedPercent = ((playerAnimate.isRunning) ? 1 : .5f) * directionalInput.x;
        animationSpeedPercent = velocity.x * directionalInput.x;

        if (player.isGrounded)
        {
            //print("Grounded");
            playerAnimate.isJumping = false;
            velocity.y = 0;
            //_yVelocity = 0;
        }
        else
        {
            //print("Gravity");
            _yVelocity += gravity * Time.deltaTime;
            //velocity.y += gravity * Time.deltaTime;
        }


        velocity.y = _yVelocity;

        player.Move(velocity * Time.deltaTime);
    }

    public void Flip()
    {
        leftRightFacing = leftRightFacing * -1;
        facingRight = !facingRight;
        this.transform.rotation = Quaternion.LookRotation(new Vector3(leftRightFacing, 0f, 0f), Vector3.up);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Inside OnTriggerEnter");
        if (other.tag == "Item")
        {
            Debug.Log("Inside Comparision of tag");
            inventory.AddItem(other.GetComponent<Item>());
            Destroy(other.gameObject);
        }
    }
}

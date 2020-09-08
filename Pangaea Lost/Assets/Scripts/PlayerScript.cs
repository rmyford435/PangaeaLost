using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine; 

public class PlayerScript : MonoBehaviour
{
    //Store a reference to all the sub player scripts
    [SerializeField]
    internal PlayerInput playerInput;

    [SerializeField]
    internal PlayerAnimate playerAnimate;

    [SerializeField]
    internal PlayerCollision playerCollision;

    [SerializeField]
    public PlayerStats PlayerStats;

    CharacterController player;

    //Movement Type Booleans (to enable/disable certain moves)
    bool movementEnabled;
    bool walkEnabled;
    bool runEnabled;
    bool crouchEnabled;
    bool proneEnabled;
    
    bool jumpEnabled;
    bool dodgeEnabled;

    float walkSpeed;
    float runSpeed;

    [SerializeField]public float animationSpeedPercent;

    float velocityXSmoothing;
    float targetVelocityX;

    [SerializeField]
    Vector3 velocity;

    [SerializeField]
    Vector3 directionalInput;

    //jump-related variables
    public float timeToJumpApex = .4f;
    public float jumpHeight = 2;
    float accelerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .1f;
    [HideInInspector]public Vector3 jumpVector;

	//New jump-related variables
	/*
	private float maxJumpVelocity;
    private float minJumpVelocity;

    private float maxJumpHeight = 4f;
    private float minJumpHeight = 1f;
    private float timeToJumpApex = .4f;
    private float accelerationTimeAirborne = .1f;
    private float accelerationTimeGrounded = .01f;

	private float _yVelocity;
	*/

    float gravity;
    float jumpVelocity; //Can remove this for new jump code

    //dash-related variables
    bool currentlyDodging = false;
	public Vector3 dodgeDirection = new Vector3(0f,0f,0f);
	private float dodgeSpeed;
	public float dodgeDistance;
	public float dodgeTimeMax = 1.0f;
	public float dodgeStepTime = 0.1f;
	private float dodgeTimeCurrent = 0.0f;

    //splineMovement-related variables


    public bool facingRight = true;
    float leftRightFacing = 1;

    public Inventory inventory;

    [SerializeField] public GameObject viewpointCamera;

    // Start is called before the first frame update
    void Start()
    {
        //PlayerStats = GetComponent<PlayerStats>;
        player = GetComponent<CharacterController>();
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
		
		//New gravity and jump-related calculations
		/*
		gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) + maxJumpVelocity);

		*/
    }

    // Update is called once per frame
    void Update()
    {
        //function grabs updated player stats from other script
        UpdatePlayerStats();

        //triggers viewpoint if player is inside viewpoint collider
        if(Input.GetButtonDown("Fire1") && viewpointCamera != null)
        {
            ToggleViewpointCamera(viewpointCamera.GetComponent<viewPointCameraCollider>().viewPointCamera.GetComponent<CinemachineVirtualCamera>());
        }

        //dodge
        if(dodgeEnabled)
        {
            DodgeTimer(); //runs dodge timer every frame
        }
        //if(Input.GetButtonDown("Fire3") && dodgeEnabled && player.isGrounded)
        if(playerAnimate.isDodging && player.isGrounded)
        {
            //reset dodge timer
            dodgeTimeCurrent = 0;
            //begin dodge
            currentlyDodging = true;
            //tell animator to play dodge animation
            playerAnimate.animator.SetBool("IsDodging", true);
            //add velocity to movement
            dodgeDirection = new Vector3((dodgeDistance * dodgeSpeed) * leftRightFacing,0f,0f);
        }
        
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
    }

    public void SetDirectionalInput(Vector3 input)
    {
        directionalInput = input;
    }

	//New jump-related functions
	/*
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

	*/

    public void Jump()
    {
        //print("Jump function was called");

        if (player.isGrounded)
        {
            print("Applying Jump Physics");
            velocity.y = jumpVelocity;
            print("velocity.y = " + velocity.y);
        }
    }

    public void DodgeTimer()
    {
        if (dodgeTimeCurrent < dodgeTimeMax)
        {
            dodgeTimeCurrent += dodgeStepTime;
        }
        else
        {
            currentlyDodging = false;
            dodgeDirection = Vector3.zero;
            playerAnimate.animator.SetBool("IsDodging", false);
        } 
    }

    void CalculateVelocity()
    {
        targetVelocityX = ((playerAnimate.isRunning) ? runSpeed : walkSpeed) * directionalInput.x;  //calculates a target velocity for x to be used in the next line

        //Applies a movement modifier, which starts at velocity.x and goes to the target velocity for x. If the player is ground it applies the first smooth time and the second if it's false.
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (player.isGrounded) ? accelerationTimeGrounded : accelerationTimeAirborne);
        /*if (Mathf.Abs(velocity.x) < .01f)
        {
            velocity.x = 0f;
        }*/
        /*if ((player.collisionFlags & CollisionFlags.Below) != 0)
        {
            print("Touching ground!");
        }*/

        //animationSpeedPercent = ((playerAnimate.isRunning) ? 1 : .5f) * directionalInput.x;
        animationSpeedPercent = velocity.x * directionalInput.x;


        if ((player.collisionFlags & CollisionFlags.Below) != 0)
        {
            //print("Touching ground!");
            velocity.y = 0;
        }

        velocity.y += gravity * Time.deltaTime; //calculates velocity.y (basically applying gravity)

        if(playerAnimate.isJumping)
        {
            Jump();
        }
		
		//New jump-related continuous calculations
		/*
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

		*/

        if(movementEnabled)
        {
            player.Move((velocity + dodgeDirection) * Time.deltaTime);
            //Debug.Log("velocity = " + velocity + "\ndodgeDirection = " + dodgeDirection);
        }
    }

    public void Flip()
    {
        leftRightFacing = leftRightFacing * -1;
        facingRight = !facingRight;
        this.transform.rotation = Quaternion.LookRotation(new Vector3(leftRightFacing, 0f, 0f), Vector3.up);
    }

    //collision based triggers
    private void OnTriggerEnter(Collider other)
    {
        //pick up item upon touching collider for item
        //Debug.Log("Inside OnTriggerEnter");
        if (other.tag == "Item")
        {
            Debug.Log("Inside Comparision of tag");
            inventory.AddItem(other.GetComponent<Item>());
            Destroy(other.gameObject);
        }
        
        if(other.tag == "Viewpoint")
        {
            other.gameObject.GetComponent<viewPointCameraCollider>().viewPointCameraEnabled = true;
            viewpointCamera = other.gameObject;
        }
    }
    private void OnTriggerStay(Collider other) 
    {
        
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.tag == "Viewpoint")
        {
            other.gameObject.GetComponent<viewPointCameraCollider>().viewPointCameraEnabled = false;
            viewpointCamera = null;
        }
    }

    void ToggleViewpointCamera(CinemachineVirtualCamera virtualCameraToChange)
    {
        Debug.Log("ToggleViewPointCamera was triggered");
        if(!virtualCameraToChange.gameObject.activeSelf)
        {
            virtualCameraToChange.gameObject.SetActive(true);
        }
        else
        {
            virtualCameraToChange.gameObject.SetActive(false);
        }
       
    }

    void UpdatePlayerStats()
    {
        //check if movement types are enabled or not based on boolean value
        movementEnabled = PlayerStats.movementEnabled;
        walkEnabled = PlayerStats.walkEnabled;
        runEnabled = PlayerStats.runEnabled;
        crouchEnabled = PlayerStats.crouchEnabled;
        proneEnabled = PlayerStats.proneEnabled;
        
        jumpEnabled = PlayerStats.jumpEnabled;
        dodgeEnabled = PlayerStats.dodgeEnabled;

        //dodge-related
        dodgeSpeed = PlayerStats.dodgeSpeed;
        dodgeTimeMax = PlayerStats.dodgeTimeMax;
        dodgeStepTime = PlayerStats.dodgeStepTime;
        dodgeDistance = PlayerStats.dodgeDistance;

        //grab updated speed values from playerStats
        walkSpeed = PlayerStats.walkSpeed;
        runSpeed = PlayerStats.runSpeed;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    PlayerScript playerScript;
    
    [SerializeField]
    public PlayerStats PlayerStats;

    [SerializeField]
    Vector3 directionalInput;

    bool crouchActive = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //running
        if(Input.GetButton("Fire3") && PlayerStats.runEnabled)
        {
            //Debug.Log("Is Running");
            playerScript.playerAnimate.isRunning = true;
        }
        else
        {
            playerScript.playerAnimate.isRunning = false;
        }
        //dodging
        if(Input.GetButtonDown("Fire3") && PlayerStats.dodgeEnabled)
        {
            //Debug.Log("Is Running");
            playerScript.playerAnimate.isDodging = true;
        }
        else
        {
            playerScript.playerAnimate.isDodging = false;
        }
        //jumping
        if (Input.GetButton("Jump") && PlayerStats.runEnabled)
        {
            playerScript.playerAnimate.isJumping = true;
            //playerScript.Jump();
        }
        else
        {
            playerScript.playerAnimate.isJumping = false;
        }
		
		//Ryan's New Jump Input
		/*
		if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Inside GetKeyDown Space");
            playerScript.OnJumpInputDown();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            Debug.Log("Inside GetKeyUp Space");
            playerScript.OnJumpInputUp();
        }
		*/
		
        //crouching
        if(Input.GetButtonDown("Crouch") && PlayerStats.crouchEnabled)
        {
            crouchActive = !crouchActive;
        }
        if (crouchActive)
        {
            playerScript.playerAnimate.isCrouching = true;
        }
        else
        {
            playerScript.playerAnimate.isCrouching = false;
        }

        //rifle firing stance
        if(Input.GetButton("Fire2"))
        {
            playerScript.playerAnimate.isFiringStance = true;
        }
        else
        {
            playerScript.playerAnimate.isFiringStance = false;
        }


        playerScript.SetDirectionalInput(directionalInput);
       
    }

    void FixedUpdate()
    {
        directionalInput = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);
    }
}

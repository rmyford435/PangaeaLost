using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    PlayerScript playerScript;

    internal bool isLeftPressed;
    internal bool isRightPressed;

    // Start is called before the first frame update
    void Start()
    {
        print("PlayerInput Starting");
        playerScript = GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 directionalInput = new Vector3(0f, Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));

        playerScript.SetDirectionalInput(directionalInput);

        if (Input.GetKey(KeyCode.A))
        {
            isLeftPressed = true;
            playerScript.playerAnimate.isMoving = true;
        }

        if (Input.GetKey(KeyCode.D))
        {
            isRightPressed = true;
            playerScript.playerAnimate.isMoving = true;
        }

        if(!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            isLeftPressed = false;
            isRightPressed = false;
            playerScript.playerAnimate.isMoving = false;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            playerScript.OnJumpInputDown();
        }

        if(Input.GetKeyUp(KeyCode.Space))
        {
            playerScript.OnJumpInputUp();
        }
   
        /*if (Input.GetKey(KeyCode.Space))
        {
            playerScript.playerAnimate.isJumping = true;
        }*/
    }
}
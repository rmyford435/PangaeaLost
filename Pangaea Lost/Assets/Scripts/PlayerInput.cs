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
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            isLeftPressed = true;
            playerScript.playerAnimate.isMoving = true;
            Debug.Log("playerScript.isMoving = " + playerScript.playerAnimate.isMoving);
            Debug.Log("A key is pressed");
        }

        if (Input.GetKey(KeyCode.D))
        {
            isRightPressed = true;
            playerScript.playerAnimate.isMoving = true;
            Debug.Log("playerScript.isMoving = " + playerScript.playerAnimate.isMoving);
            Debug.Log("D key is pressed");
        }

        if(!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            isLeftPressed = false;
            isRightPressed = false;
            playerScript.playerAnimate.isMoving = false;
            Debug.Log("playerScript.isMoving = " + playerScript.playerAnimate.isMoving);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            playerScript.playerAnimate.isJumping = true;
        }
        else
        {
            playerScript.playerAnimate.isJumping = false;
        }
    }
}
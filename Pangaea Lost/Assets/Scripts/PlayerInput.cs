using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    PlayerScript playerScript;

    internal bool isLeftPressed;
    internal bool isRightPressed;
    internal bool isJumping;

    

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
            Debug.Log("A key is pressed");
        }
        else
        {
            isLeftPressed = false;
        }

        if (Input.GetKey(KeyCode.D))
        {
            isRightPressed = true;
            Debug.Log("D key is pressed");
        }
        else
        {
            isRightPressed = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
        }
        else
        {
            isJumping = false;
        }
    }
}
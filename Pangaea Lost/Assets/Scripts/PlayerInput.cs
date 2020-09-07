using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    PlayerScript playerScript;

    [SerializeField]
    Vector3 directionalInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            //Debug.Log("Is Running");
            playerScript.playerAnimate.isRunning = true;
        }
        else
        {
            playerScript.playerAnimate.isRunning = false;
        }

        playerScript.SetDirectionalInput(directionalInput);

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
    }

    void FixedUpdate()
    {
        directionalInput = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);
    }
}

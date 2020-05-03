using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimate : MonoBehaviour
{
    [SerializeField]
    PlayerScript playerScript;

    Animator animator;

    public bool isCrouching = false;
    public bool isMoving = false;
    public bool isGrounded = false;
    public bool isJumping = false;
    public bool isFalling = false;
    public bool isRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerScript.input.z > 0 || playerScript.input.z < 0)
        {
            Debug.Log("Inside animate walk");
            Debug.Log("playerScript.isMoving = " + isMoving);
            animator.SetBool("isMoving", isMoving);
            animator.SetFloat("velocity.z", playerScript.Velocity.z);
        }
        else if(playerScript.input.z == 0)
        {
            Debug.Log("Inside not animate walk");
            animator.SetBool("isMoving", isMoving);
            animator.SetFloat("velocity.z", 0);
        }

        if(isJumping)
        {
            animator.SetBool("isJumping", isJumping);
        }
    }
}

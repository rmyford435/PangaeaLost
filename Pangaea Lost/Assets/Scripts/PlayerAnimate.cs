using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimate : MonoBehaviour
{
    [SerializeField]
    PlayerScript playerScript;

    public Animator animator;

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
        if(playerScript.directionalInput.z > 0 || playerScript.directionalInput.z < 0)
        {
            animator.SetBool("isMoving", isMoving);
            animator.SetFloat("velocity.z", playerScript.Velocity.z);
        }
        else if(playerScript.directionalInput.z == 0)
        {
            animator.SetBool("isMoving", isMoving);
            animator.SetFloat("velocity.z", 0);
        }

        /*if(isJumping && isGrounded)
        {
            animator.SetBool("isJumping", isJumping);
            isGrounded = false;
            animator.SetBool("isGrounded", isGrounded);
        }*/

        if (playerScript.playerCollision.collisions.below)
        {
            isGrounded = true;
            animator.SetBool("isGrounded", isGrounded);
        }
    }
}

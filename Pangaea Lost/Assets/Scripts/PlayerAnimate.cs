﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimate : MonoBehaviour
{
    [SerializeField]
    PlayerScript playerScript;

    public Animator animator;

    public bool isRunning;
    public bool isJumping;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isRunning)
        {
            animator.SetFloat("speedPercent", playerScript.animationSpeedPercent);
        }
        else
        {
            animator.SetFloat("speedPercent", playerScript.animationSpeedPercent);
        }
        

    }
}

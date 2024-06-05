using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMoveControls : MonoBehaviour
{
    public float moveSpeed = 0f;
    public Animator anim;
    private Vector3 centralPos;
    private Vector3 moveDirection = Vector3.forward;
    private bool isJumping = false;
    private bool isRolling = false;
    private bool turnL = false;
    private bool turnR = false;
    private int numFramesJump = 0;
    private int numFramesRoll = 0;
    private int lane = 1;
    private float pos;
    private void Awake()
    {
    }
    private void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        //----------------LEFT-RIGHT-MOVEMENT----------------
        if (Input.GetKeyDown("right") && !turnR && !turnL)
        {
            if (lane < 2) {
                turnR = true;
                pos = 0;
                ++lane;
            }
        }

        if (Input.GetKeyDown("left") && !turnL && !turnR)
        {
            if (lane > 0) {
                turnL = true;
                pos = 0;
                --lane;
            }
        }

        //----------------JUMP-ROLL----------------
        if (Input.GetKeyDown("up") && !isJumping && !isRolling)
        {
            anim.SetTrigger("Jump");
            isJumping = true;
        }

        if (Input.GetKeyDown("down") && !isJumping && !isRolling)
        {
            anim.SetTrigger("Roll");
            isRolling = true;
        }
    }
    private void FixedUpdate()
    {
        if (turnR && pos <= 9)
        {
            transform.Translate(Vector3.Lerp(new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.9f * 7, 0.0f, 0.0f), 0.2f), Space.Self);
            ++pos;
        }
        else turnR = false;

        if (turnL && pos <= 9)
        {
            transform.Translate(Vector3.Lerp(new Vector3(0.0f, 0.0f, 0.0f), new Vector3(-0.9f * 7, 0.0f, 0.0f), 0.2f), Space.Self);
            ++pos;
        }
        else turnL = false;

        if (isJumping)
        {
            ++numFramesJump;
            if (numFramesJump >= 40) 
            { 
                isJumping = false;
                numFramesJump = 0;
            }
        }
        if (isRolling)
        {
            ++numFramesRoll;
            if (numFramesRoll >= 40)
            {
                isRolling = false;
                numFramesRoll = 0;
            }
        } 
    }
}

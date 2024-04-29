using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 3;
    public Animator anim;
    public BoxCollider collisionJump;
    public BoxCollider collisionRoll;
    private bool isJumping = false;
    private bool isRolling = false;

    private float timeJ = 0f;
    private float timeR = 0f;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.World);

        if(Input.GetKeyDown("right"))
        {
            transform.position = transform.position + new Vector3(0.9f, 0.0f, 0.0f);
            //transform.Translate(Vector3.right * Time.deltaTime * moveSpeed, Space.World);
        }

        else if (Input.GetKeyDown("left"))
        {
            transform.position = transform.position + new Vector3(-0.9f, 0.0f, 0.0f);
        }

        else if (Input.GetKeyDown("up") && !isJumping && !isRolling)
        {
            anim.SetTrigger("Jump");
            isJumping = true;
            timeJ = Time.time;
            collisionJump.enabled = false;
        }

        else if (Input.GetKeyDown("down") && !isJumping && !isRolling)
        {
            anim.SetTrigger("Roll");
            isRolling = true;
            timeR = Time.time;
            collisionRoll.enabled = false;
        }

        if(Time.time - timeJ >= 0.933 && isJumping)
        {
            isJumping = false;
            Debug.Log("desactivar isJumping");
            timeJ = 0f;
            collisionJump.enabled = true;
        }
        if (Time.time - timeR >= 1.167 && isRolling)
        {
            isRolling = false;
            Debug.Log("desactivar isRolling");
            timeR = 0f;
            collisionRoll.enabled = true;
        }
    }
}

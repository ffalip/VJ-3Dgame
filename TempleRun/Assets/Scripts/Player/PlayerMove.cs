using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 3;
    public float turnSpeed = 0.1f;
    private float centralAxis = 0;
    public Animator anim;
    public BoxCollider collisionJump;
    public BoxCollider collisionRoll;
    private Vector3 moveDirection = Vector3.forward;
    private bool isJumping = false;
    private bool isRolling = false;
    private bool turnL = false;
    private bool turnR = false;
    private bool collisionTurnR = false;
    private bool collisionTurnL = false;
    private float obj;
    private float pos;
    private float timeJ = 0f;
    private float timeR = 0f;
    public GameObject camera;
    // Update is called once per frame
    public Vector3 startMarker;
    public Vector3 endMarker;
    void Update()
    {
        //----------------LEFT-RIGHT-MOVEMENT----------------
        if(Input.GetKeyDown("right") && !turnR && !turnL && !collisionTurnR && !collisionTurnL)
        {
            if (moveDirection == Vector3.forward || moveDirection == Vector3.back){
                if (transform.position.x < centralAxis + 0.9f)
                    turnR = true;
                    pos = transform.position.x;
            } else {
                if (transform.position.z != centralAxis + 0.9f)
                    turnR = true;
                    pos = transform.position.z;
            }
        }
        else if (Input.GetKeyDown("left") && !turnL && !turnR && !collisionTurnR && !collisionTurnL)
        {
            if (moveDirection == Vector3.forward || moveDirection == Vector3.back){
                if (transform.position.x > centralAxis - 0.9f) {
                    turnL = true;
                    pos = transform.position.x;
                }
            } else {
                if (transform.position.z > centralAxis - 0.9f)
                    turnL = true;
                    pos = transform.position.z;
            }
        }
        //----------------JUMP-ROLL----------------
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

        if(Time.time - timeJ >= 0.5 && isJumping)
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


        if (collisionTurnL && Input.GetKeyDown("left"))
        {
            collisionTurnL = false;
            transform.Rotate(0.0f, -90.0f, 0.0f, Space.Self);
            
            
            if (moveDirection == Vector3.forward)
            {
                centralAxis = transform.position.x;
                moveDirection = Vector3.left;
            }
            else if (moveDirection == Vector3.left)
            {
                centralAxis = transform.position.z;
                moveDirection = Vector3.back;
            }
            else if (moveDirection == Vector3.back)
            {
                centralAxis = transform.position.x;
                moveDirection = Vector3.right;
            }
            else
            {
                centralAxis = transform.position.z;
                moveDirection = Vector3.forward;
            }
            camera.GetComponent<CameraFollowPlayer>().modifyOffset(moveDirection);
        }

        if (collisionTurnR && Input.GetKeyDown("right"))
        {
            collisionTurnR = false;
            transform.Rotate(0.0f, 90.0f, 0.0f, Space.Self);
           
            if (moveDirection == Vector3.forward)
            {
                centralAxis = transform.position.x;
                moveDirection = Vector3.right;
            }
            else if (moveDirection == Vector3.left)
            {
                centralAxis = transform.position.z;
                moveDirection = Vector3.forward;
            }
            else if (moveDirection == Vector3.back)
            {
                centralAxis = transform.position.x;
                moveDirection = Vector3.left;
            }
            else
            {
                centralAxis = transform.position.z;
                moveDirection = Vector3.back;
            }
            camera.GetComponent<CameraFollowPlayer>().modifyOffset(moveDirection);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Time.timeScale = 0;
        }
    }
    private void FixedUpdate() {
        //transform.Translate(moveDirection * Time.deltaTime * moveSpeed, Space.World);
        if (turnL && transform.position.x <= pos - 0.9f) {
            turnL = false;
            transform.position = new Vector3(pos - 0.9f, transform.position.y, transform.position.z); 
        } else if(turnL){
            Vector3 turnDirection = new Vector3(1.0f, 0.0f ,1.0f) - new Vector3(Mathf.Abs(moveDirection.x), 0, Mathf.Abs(moveDirection.z));
            transform.position = transform.position + turnDirection * turnSpeed * (-1); 
        }
        if (turnR && transform.position.x >= pos + 0.9f) {
            turnR = false;
            transform.position = new Vector3(pos + 0.9f, transform.position.y, transform.position.z); 
        } else if(turnR) {
            Vector3 turnDirection = new Vector3(1.0f, 0.0f ,1.0f) - new Vector3(Mathf.Abs(moveDirection.x), 0, Mathf.Abs(moveDirection.z));
            transform.position = transform.position + turnDirection * turnSpeed; 
        }
        transform.Translate(moveDirection * Time.deltaTime * moveSpeed, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "HitBoxLeft")
        {
            collisionTurnL = true;
            Debug.Log("LEft");
        }
        if (other.tag == "HitBoxRight")
        {
            collisionTurnR = true;
            Debug.Log("Right");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "HitBoxLeft")
        {
            collisionTurnL = false;
            Debug.Log("LEftExit");
        }
        if (other.tag == "HitBoxRight")
        {
            collisionTurnR = false;
            Debug.Log("RightExit");
        }
    }
}

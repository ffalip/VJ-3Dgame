using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 2.7f;
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
    private int lane = 1;
    private float pos;
    private int numFramesJump = 0;
    private int numFramesRoll = 0;
    private int numFramesTrip = 0;
    private Vector3 turnPos;
    public GameObject camera;
    public GameObject esq;
    private bool isEntrebancat = false;
    private bool isDead = false;
    public GameObject LevelControl;

    public TextMeshProUGUI deadText;
    private void Awake()
    {
        deadText.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        //----------------LEFT-RIGHT-MOVEMENT----------------
        if (Input.GetKeyDown("right") && !turnR && !turnL && !collisionTurnR && !collisionTurnL &&!isDead)
        {
            turnR = true;
            pos = 0;
        }

        if (Input.GetKeyDown("left") && !turnL && !turnR && !collisionTurnR && !collisionTurnL &&!isDead)
        {
            turnL = true;
            pos = 0;
        }

        //----------------JUMP-ROLL----------------
        if (Input.GetKeyDown("up") && !isJumping && !isRolling && !isDead)
        {
            anim.SetTrigger("Jump");
            isJumping = true;
            collisionJump.enabled = false;
        }

        if (Input.GetKeyDown("down") && !isJumping && !isRolling && !isDead)
        {
            anim.SetTrigger("Roll");
            isRolling = true;
            collisionRoll.enabled = false;
        }


        //----------------TURN-LEFT---------------
        if (collisionTurnL && Input.GetKeyDown("left") && !isDead)
        {
            collisionTurnL = false;
            transform.Rotate(0.0f, -90.0f, 0.0f, Space.Self);

            float dirX = 1;
            float dirZ = 0;
            if (moveDirection == Vector3.back || moveDirection == Vector3.forward)
            {
                dirX = 0;
                dirZ = 1;
            }

            if (moveDirection == Vector3.forward) moveDirection = Vector3.left;
            else if (moveDirection == Vector3.left) moveDirection = Vector3.back;
            else if (moveDirection == Vector3.back) moveDirection = Vector3.right;
            else moveDirection = Vector3.forward;

            transform.position = transform.position + new Vector3(-(transform.position.x % 0.9f) + 0.9f * dirX, 0f, -(transform.position.z % 0.9f) + 0.9f * dirZ);
            camera.GetComponent<CameraFollowPlayer>().modifyOffset(moveDirection);
            esq.GetComponent<EsqController>().modifyMoveDirection(moveDirection, transform.rotation);
        }
        //----------------TURN-RIGHT---------------
        if (collisionTurnR && Input.GetKeyDown("right") && !isDead)
        {
            collisionTurnR = false;
            transform.Rotate(0.0f, 90.0f, 0.0f, Space.Self);

            float dirX = 1;
            float dirZ = 0;
            if (moveDirection == Vector3.back || moveDirection == Vector3.forward)
            {
                dirX = 0;
                dirZ = 1;
            }

            if (moveDirection == Vector3.forward) moveDirection = Vector3.right;
            else if (moveDirection == Vector3.left) moveDirection = Vector3.forward;
            else if (moveDirection == Vector3.back) moveDirection = Vector3.left;
            else moveDirection = Vector3.back;

            transform.position = transform.position + new Vector3(-(transform.position.x % 0.9f) + 0.9f * dirX, 0f, -(transform.position.z % 0.9f) + 0.9f * (dirZ));

            camera.GetComponent<CameraFollowPlayer>().modifyOffset(moveDirection);
            esq.GetComponent<EsqController>().modifyMoveDirection(moveDirection, transform.rotation);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Time.timeScale = 0;
        }
        if (isDead && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Credits");
        }
    }
    private void FixedUpdate()
    {
        if (turnR && pos <= 9)
        {
            transform.Translate(Vector3.Lerp(new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.9f, 0.0f, 0.0f), 0.1f), Space.Self);
            ++pos;
        }
        else turnR = false;

        if (turnL && pos <= 9)
        {
            transform.Translate(Vector3.Lerp(new Vector3(0.0f, 0.0f, 0.0f), new Vector3(-0.9f, 0.0f, 0.0f), 0.1f), Space.Self);
            ++pos;
        }
        else turnL = false;

        if (isJumping && !isDead)
        {
            ++numFramesJump;
            if (numFramesJump >= 50) 
            { 
                isJumping = false;
                numFramesJump = 0;
                collisionJump.enabled = true;
            }
        }
        if (isRolling && !isDead)
        {
            ++numFramesRoll;
            if (numFramesRoll >= 50)
            {
                isRolling = false;
                numFramesRoll = 0;
                collisionRoll.enabled = true;
            }
        }
        if (isEntrebancat && !isDead)
        {
            ++numFramesTrip;
            if (numFramesTrip >= 250)
            {
                isEntrebancat = false;
                numFramesTrip = 0;
                LevelControl.GetComponent<GenerateLevel>().changeIsTrip();
                moveSpeed = 2.7f;
            }
        }
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "HitBoxLeft") collisionTurnL = true;

        if (other.tag == "HitBoxRight") collisionTurnR = true;

        if (other.tag == "Paret") die();

        turnPos = transform.position;
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

    private void die()
    {
        isDead = true;
        moveSpeed = 0.0f;
        anim.SetTrigger("Crash");
        deadText.enabled = true;
        LevelControl.GetComponent<GenerateLevel>().setPlayerIsDead();
    }

    public void trip()
    {
        if (isEntrebancat) die();
        else
        {
            if (!isDead)
            {
                anim.SetTrigger("Trip");
                isEntrebancat = true;
                moveSpeed = 1.35f;
                LevelControl.GetComponent<GenerateLevel>().changeIsTrip();
            }
        }
    }

    public bool getIsDead()
    {
        return isDead;
    }

    public bool getIsTrip()
    {
        return isEntrebancat;
    }
    
}

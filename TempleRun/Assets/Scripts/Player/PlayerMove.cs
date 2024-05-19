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
    private float timeJ = 0f;
    private float timeR = 0f;
    private Vector3 turnPos;
    public GameObject camera;
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
            timeJ = Time.time;
            collisionJump.enabled = false;
        }
        if (Input.GetKeyDown("down") && !isJumping && !isRolling && !isDead)
        {
            anim.SetTrigger("Roll");
            isRolling = true;
            timeR = Time.time;
            collisionRoll.enabled = false;
        }

        if (Time.time - timeJ >= 0.5 && isJumping)
        {
            isJumping = false;
            timeJ = 0f;
            collisionJump.enabled = true;
        }

        if (Time.time - timeR >= 1.167 && isRolling)
        {
            isRolling = false;
            timeR = 0f;
            collisionRoll.enabled = true;
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
            Debug.Log("moveDir:" + moveDirection);
            if (moveDirection == Vector3.forward) moveDirection = Vector3.left;
            else if (moveDirection == Vector3.left) moveDirection = Vector3.back;
            else if (moveDirection == Vector3.back) moveDirection = Vector3.right;
            else moveDirection = Vector3.forward;

            transform.position = transform.position + new Vector3(-(transform.position.x % 0.9f) + 0.9f * dirX, 0f, -(transform.position.z % 0.9f) + 0.9f * dirZ);
            camera.GetComponent<CameraFollowPlayer>().modifyOffset(moveDirection);
        }
        //----------------TURN-RIGHT---------------
        if (collisionTurnR && Input.GetKeyDown("right") && !isDead)
        {
            collisionTurnR = false;
            transform.Rotate(0.0f, 90.0f, 0.0f, Space.Self);

            float dirX = 0;
            float dirZ = 0;
            Debug.Log("moveDir -> " + moveDirection);
            if (moveDirection == Vector3.forward)
            {
                moveDirection = Vector3.right;
                dirX = 0;
                dirZ = 1;
            }
            else if (moveDirection == Vector3.left)
            {
                moveDirection = Vector3.forward;
                dirX = -1;
                dirZ = 0;
            }
            else if (moveDirection == Vector3.back)
            {
                moveDirection = Vector3.left;
                dirX = 0;
                dirZ = 1;
            }
            else
            {
                moveDirection = Vector3.back;
                dirX = 1;
                dirZ = 0;
            }

            transform.position = transform.position + new Vector3(-(transform.position.x % 0.9f) + 0.9f * dirX, 0f, -(transform.position.z % 0.9f) + 0.9f * (dirZ));

            camera.GetComponent<CameraFollowPlayer>().modifyOffset(moveDirection);
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
        }
        if (other.tag == "HitBoxRight")
        {
            collisionTurnR = false;
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
}

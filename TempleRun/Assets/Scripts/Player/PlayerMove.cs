using System;
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
    public BoxCollider collisionFall;
    private Vector3 centralPos;
    private Vector3 moveDirection = Vector3.forward;
    private bool isJumping = false;
    private bool isRolling = false;
    private bool turnL = false;
    private bool turnR = false;
    private bool collisionTurnR = false;
    private bool collisionTurnL = false;
    private bool canTurn = false;
    private bool alreadyTurned = false;
    private int lane = 1;
    private float pos;
    private int numFramesJump = 0;
    private int numFramesJumpHitbox = 0;
    private int numFramesRoll = 0;
    private int numFramesRollHitbox = 0;
    private int numFramesTrip = 0;
    public GameObject camera;
    public GameObject esq;
    private bool isEntrebancat = false;
    private bool isDead = false;
    private bool isFall = false;
    private bool godMode = false;
    public GameObject LevelControl;
    public GameManager gm;
    public TextMeshProUGUI deadText;
    public AudioSource running;
    public AudioSource skeletonSound;
    public AudioSource watterSound;
    public AudioSource hitSound;
    private bool increase = true;
    public ParticleSystem water;
    public ParticleSystem coinEffect;
    private int count = 0;

    private void Awake()
    {
        deadText.enabled = false;
        running.Play();
    }
    private void Start()
    {
        count = 0;
        godMode = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) {
            godMode = !godMode;
            if (godMode) Debug.Log("godmodeActivat");
        }
        //----------------LEFT-RIGHT-MOVEMENT----------------
        if (Input.GetKeyDown("right") && !turnR && !turnL && !collisionTurnR && !collisionTurnL &&!isDead && !isFall)
        {
            if (lane < 2) {
                turnR = true;
                pos = 0;
            }
        }

        if (Input.GetKeyDown("left") && !turnL && !turnR && !collisionTurnR && !collisionTurnL &&!isDead && !isFall)
        {
            if (lane > 0) {
                turnL = true;
                pos = 0;
            }
        }

        //----------------JUMP-ROLL----------------
        if (Input.GetKeyDown("up") && !isJumping && !isRolling && !isDead && !isFall)
        {
            anim.SetTrigger("Jump");
            isJumping = true;
            collisionJump.enabled = false;
            collisionFall.enabled = false;
            running.Stop();
        }

        if (Input.GetKeyDown("down") && !isJumping && !isRolling && !isDead && !isFall)
        {
            anim.SetTrigger("Roll");
            isRolling = true;
            collisionRoll.enabled = false;
            running.Stop();
        }


        //----------------TURN-LEFT---------------
        if (collisionTurnL && Input.GetKeyDown("left") && !isDead && !isFall && canTurn && !alreadyTurned)
        {
            collisionTurnL = false;
            canTurn = false;
            alreadyTurned = true;
            transform.Rotate(0.0f, -90.0f, 0.0f, Space.Self);

            if (moveDirection == Vector3.forward)
            {
                moveDirection = Vector3.left;
            }
            else if (moveDirection == Vector3.left)
            {
                moveDirection = Vector3.back;
            }
            else if (moveDirection == Vector3.back)
            {
                moveDirection = Vector3.right;
            }
            else
            {
                moveDirection = Vector3.forward;
            }
            float lane = 0;
            transform.position = new Vector3(centralPos.x, transform.position.y, centralPos.z);
            //transform.position = transform.position + new Vector3(-(transform.position.x % 0.9f) + 0.9f * lane, 0f, -(transform.position.z % 0.9f) + 0.9f * lane);
            camera.GetComponent<CameraFollowPlayer>().modifyOffset(moveDirection);
            esq.GetComponent<EsqController>().modifyMoveDirection(moveDirection, transform.rotation);
        }
        //----------------TURN-RIGHT---------------
        if (collisionTurnR && Input.GetKeyDown("right") && !isDead && !isFall && canTurn && !alreadyTurned)
        {
            collisionTurnR = false;
            canTurn = false;
            alreadyTurned = true;
            transform.Rotate(0.0f, 90.0f, 0.0f, Space.Self);
            
            Debug.Log("moveDir -> " + moveDirection);
            if (moveDirection == Vector3.forward)
            {
                moveDirection = Vector3.right;
            }
            else if (moveDirection == Vector3.left)
            {
                moveDirection = Vector3.forward;
            }
            else if (moveDirection == Vector3.back)
            {
                moveDirection = Vector3.left;
            }
            else
            {
                moveDirection = Vector3.back;
            }
            lane = 0;
            transform.position = new Vector3(centralPos.x, transform.position.y, centralPos.z);
            //transform.position = transform.position + new Vector3(-(transform.position.x % 0.9f) + 0.9f * lane, 0f, -(transform.position.z % 0.9f) + 0.9f * (lane));

            camera.GetComponent<CameraFollowPlayer>().modifyOffset(moveDirection);
            esq.GetComponent<EsqController>().modifyMoveDirection(moveDirection, transform.rotation);
        }

        if ((isDead || isFall) && Input.GetKeyDown(KeyCode.R))
        {
            watterSound.Stop();
            running.Stop();
            skeletonSound.Stop();
            gm.GameScene("Credits");
            Time.timeScale = 1;
        }
    }
    private void FixedUpdate()
    {
        if (running.isPlaying && !isDead) {
            if (running.panStereo <= -0.6f) increase = true;
            else if (running.panStereo >= 0.6f) increase = false;

            if (increase )running.panStereo += 0.002f;
            else running.panStereo -= 0.002f;
        } else running.Stop();

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

        if (isJumping && !isDead && !isFall)
        {
            ++numFramesJump;
            if (numFramesJump >= 40) 
            { 
                isJumping = false;
                numFramesJump = 0;
                collisionJump.enabled = true;
                collisionFall.enabled = true;
                running.Play();
            }
        }
        if (isRolling && !isDead && !isFall)
        {
            ++numFramesRoll;
            if (numFramesRoll >= 40)
            {
                isRolling = false;
                collisionRoll.enabled = true;
                numFramesRoll = 0;
                collisionRoll.enabled = true;
                running.Play();
            }
        }
        if (isEntrebancat && !isDead && !isFall)
        {
            ++numFramesTrip;
            if (numFramesTrip >= 250)
            {
                skeletonSound.Stop();
                isEntrebancat = false;
                numFramesTrip = 0;
                LevelControl.GetComponent<GenerateLevel>().changeIsTrip();
                moveSpeed = 2.7f;
                running.pitch = 1;
            }
        }
        if (alreadyTurned && count >= 100) {
            alreadyTurned = false;
            count = 0;
        }
        else if (alreadyTurned) count += 1; 

        if (Time.timeScale < 2f) Time.timeScale += 0.00005f;
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.Self);
        water.GetComponent<Transform>().position = new Vector3( transform.position.x, transform.position.y -1, transform.position.z ) + moveDirection;
        coinEffect.GetComponent<Transform>().position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "HitBoxLeft") {
            collisionTurnL = true;
        }
        if (other.tag == "HitboxLeftLane0")
        {
            canTurn = true;
            centralPos = other.transform.position;
            lane = 0;
        } else if (other.tag == "HitboxLeftLane1")
        {
            canTurn = true;
            centralPos = other.transform.position;
            lane = 1;
        } else if (other.tag == "HitboxLeftLane2")
        {
            canTurn = true;
            centralPos = other.transform.position;
            lane = 2;
        }

        if (other.tag == "HitBoxRight") {
            collisionTurnR = true;
        }
        if (other.tag == "HitboxRightLane0")
        {
            canTurn = true;
            centralPos = other.transform.position;
            lane = 0;
        } else if (other.tag == "HitboxRightLane1")
        {
            canTurn = true;
            centralPos = other.transform.position;
            lane = 1;
        } else if (other.tag == "HitboxRightLane2")
        {
            canTurn = true;
            centralPos = other.transform.position;
            lane = 2;
        }

        if (other.tag == "Paret") {
            hitSound.Play();
            die();
        }
        if (other.tag == "Right") lane = 2;
        if (other.tag == "Center") lane = 1;
        if (other.tag == "Left") lane = 0;

        if (godMode && other.tag == "TriggerJump" && !isJumping && !isRolling && !isDead && !isFall)
        {
            anim.SetTrigger("Jump");
            isJumping = true;
            collisionJump.enabled = false;
            collisionFall.enabled = false;
        }

        if (godMode && other.tag == "TriggerRoll" && !isJumping && !isRolling && !isDead && !isFall)
        {
            anim.SetTrigger("Roll");
            isRolling = true;
            collisionRoll.enabled = false;
        }

        if (collisionTurnL && godMode && other.tag == "TriggerTurnL" && !isJumping && !isRolling && !isDead && !isFall && canTurn)
        {
            collisionTurnL = false;
            canTurn = false;
            transform.Rotate(0.0f, -90.0f, 0.0f, Space.Self);

            if (moveDirection == Vector3.forward)
            {
                moveDirection = Vector3.left;
            }
            else if (moveDirection == Vector3.left)
            {
                moveDirection = Vector3.back;
            }
            else if (moveDirection == Vector3.back)
            {
                moveDirection = Vector3.right;
            }
            else
            {
                moveDirection = Vector3.forward;
            }
            float lane = 0;
            transform.position = new Vector3(centralPos.x, transform.position.y, centralPos.z);
            //transform.position = transform.position + new Vector3(-(transform.position.x % 0.9f) + 0.9f * lane, 0f, -(transform.position.z % 0.9f) + 0.9f * lane);
            camera.GetComponent<CameraFollowPlayer>().modifyOffset(moveDirection);
            esq.GetComponent<EsqController>().modifyMoveDirection(moveDirection, transform.rotation);
        }
    
        if (collisionTurnR && godMode && other.tag == "TriggerTurnR" && !isJumping && !isRolling && !isDead && !isFall && canTurn)
        {
            collisionTurnR = false;
            canTurn = false;
            transform.Rotate(0.0f, 90.0f, 0.0f, Space.Self);

            Debug.Log("moveDir -> " + moveDirection);
            if (moveDirection == Vector3.forward)
            {
                moveDirection = Vector3.right;
            }
            else if (moveDirection == Vector3.left)
            {
                moveDirection = Vector3.forward;
            }
            else if (moveDirection == Vector3.back)
            {
                moveDirection = Vector3.left;
            }
            else
            {
                moveDirection = Vector3.back;
            }
            lane = 0;
            transform.position = new Vector3(centralPos.x, transform.position.y, centralPos.z);
            //transform.position = transform.position + new Vector3(-(transform.position.x % 0.9f) + 0.9f * lane, 0f, -(transform.position.z % 0.9f) + 0.9f * (lane));

            camera.GetComponent<CameraFollowPlayer>().modifyOffset(moveDirection);
            esq.GetComponent<EsqController>().modifyMoveDirection(moveDirection, transform.rotation);
        }

        if (godMode && other.tag == "TriggerDreta" && !turnR && !turnL && !collisionTurnR && !collisionTurnL && !isDead && !isFall)
        {
            if (lane < 2)
            {
                turnR = true;
                pos = 0;
            }
        }

        if (godMode && other.tag == "TriggerEsquerra" && !turnL && !turnR && !collisionTurnR && !collisionTurnL && !isDead && !isFall)
        {
            if (lane > 0)
            {
                turnL = true;
                pos = 0;
            }
        }
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

    public void die()
    {
        
        isDead = true;
        moveSpeed = 0.0f;
        anim.SetTrigger("Crash");
        deadText.enabled = true;
        LevelControl.GetComponent<GenerateLevel>().setPlayerIsDead();
    }

    public void fall()
    {
        watterSound.Play();
        running.Stop();
        isFall = true;
        moveSpeed = 0.0f;
        anim.SetTrigger("Fall");
        deadText.enabled = true;
        LevelControl.GetComponent<GenerateLevel>().setPlayerIsDead();
        water.Play();
    }

    public void trip()
    {
        if (isEntrebancat) die();
        else
        {
            if (!isDead)
            {
                running.pitch = 0.92f;
                skeletonSound.Play();
                anim.SetTrigger("Trip");
                isEntrebancat = true;
                moveSpeed = 1.8f;
                LevelControl.GetComponent<GenerateLevel>().changeIsTrip();
            }
        }
    }

    public bool getIsDead()
    {
        return isDead;
    }

    public bool getIsFall()
    {
        return isFall;
    }

    public bool getIsTrip()
    {
        return isEntrebancat;
    }
    public Vector3 getDirection()
    {
        return moveDirection;
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EsqController : MonoBehaviour
{
    public Transform cameraPos;
    public Transform playerPos;
    public PlayerMove pm;
    public float smoothTime;
    public Animator esqAnim;
    private Vector3 moveDirection = Vector3.forward;
    private Vector3 targetPos;
    private Vector3 currentVelocity = Vector3.zero;
    private int numFramesEat = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float t = Time.deltaTime * smoothTime;
        targetPos = new Vector3(cameraPos.position.x, transform.position.y, cameraPos.position.z) + moveDirection * 1.5f;
        if (pm.getIsTrip())
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, t);
        }
        else
        {
            if (pm.getIsDead())
            {
                transform.position = Vector3.Lerp(transform.position, playerPos.position - moveDirection, t);
            }
            else transform.position = Vector3.Lerp(transform.position, targetPos - moveDirection*3f, t);
        }
        if (pm.getIsDead())
        {
            ++numFramesEat;
            if (numFramesEat >= 30)
            {
                esqAnim.SetTrigger("Eat");
                numFramesEat = 0;
            }
        }
    }

    public void modifyMoveDirection(Vector3 md, Quaternion dir)
    {
        moveDirection = md;
        transform.rotation = dir;
    }

}

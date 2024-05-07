using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    private Vector3 offset;
    Quaternion direction;
    public Transform target;
    public float smoothTime1;
    public float smoothTime2;
    private Vector3 currentVelocity = Vector3.zero;

    private void Awake()
    {
        offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime1);
        transform.rotation = Quaternion.Slerp(transform.rotation, direction, smoothTime2);
        //transform.rotation = Quaternion.Euler(Vector3.SmoothDamp(transform.rotation.eulerAngles, direction.eulerAngles, ref currentVelocity, smoothTime2 * Time.deltaTime));
    }

    public void modifyOffset(Vector3 dir)
    {

        if (dir == Vector3.forward)
        {
            offset = new Vector3(0.0f, 1.5f, -3.0f);
            direction = Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f));
        }
        else if (dir == Vector3.right)
        {
            offset = new Vector3(-3.0f, 1.5f, 0.0f);
            direction = Quaternion.Euler(new Vector3(0.0f, 90.0f, 0.0f));
        }
        else if (dir == Vector3.left)
        {
            offset = new Vector3(3.0f, 1.5f, 0.0f);
            direction = Quaternion.Euler(new Vector3(0.0f, -90.0f, 0.0f));
        }
        else
        {
            offset = new Vector3(0.0f, 1.5f, 3.0f);
            direction = Quaternion.Euler(new Vector3(0.0f, 180.0f, 0.0f));
        }
        
    }
}

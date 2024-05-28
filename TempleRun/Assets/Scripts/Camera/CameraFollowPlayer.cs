using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Vector3 offset;
    public Quaternion direction;
    public Transform target;
    public float smoothTime1;
    public float smoothTime2;
    private Vector3 currentVelocity = Vector3.zero;

    private void Awake()
    {
        offset = transform.position - target.position;
    }

    private void FixedUpdate()
    {
        float t = Time.deltaTime * smoothTime2;
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime1);
        transform.rotation = Quaternion.SlerpUnclamped(transform.rotation, direction, t);

    }

    public void modifyOffset(Vector3 dir)
    {

        if (dir == Vector3.forward)
        {
            offset = new Vector3(0.0f, 1.85f, -1.85f);
            direction = Quaternion.Euler(new Vector3(20.0f, 0.0f, 0.0f));
        }
        else if (dir == Vector3.right)
        {
            offset = new Vector3(-1.85f, 1.85f, 0.0f);
            direction = Quaternion.Euler(new Vector3(20.0f, 90.0f, 0.0f));
        }
        else if (dir == Vector3.left)
        {
            offset = new Vector3(1.85f, 1.85f, 0.0f);
            direction = Quaternion.Euler(new Vector3(20.0f, -90.0f, 0.0f));
        }
        else
        {
            offset = new Vector3(0.0f, 1.85f, 1.85f);
            direction = Quaternion.Euler(new Vector3(20.0f, 180.0f, 0.0f));
        }
        
    }
}

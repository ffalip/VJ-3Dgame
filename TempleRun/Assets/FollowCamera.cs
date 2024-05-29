using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform cam;
    public Transform player;
    private Vector3 dir = Vector3.forward;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dir = player.GetComponent<PlayerMove>().getDirection();
        if (dir == Vector3.forward)
        {
            transform.position = new Vector3(cam.position.x - 1.1f, cam.position.y-0.05f, cam.position.z+1.35f);
        }
        else if (dir == Vector3.right)
        {
            transform.position = new Vector3(cam.position.x + 1.35f, cam.position.y - 0.05f, cam.position.z + 1.1f);
        }
        else if (dir == Vector3.left)
        {
            transform.position = new Vector3(cam.position.x-1.35f, cam.position.y - 0.05f, cam.position.z - 1.1f);
        }
        else
        {
            transform.position = new Vector3(cam.position.x + 1.1f, cam.position.y - 0.05f, cam.position.z - 1.35f);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followLight : MonoBehaviour
{
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + new Vector3(0f, 2f, 0f);
    }
}

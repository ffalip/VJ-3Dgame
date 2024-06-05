using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColLideJump : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerMove pm;

    int iframes = 0;
    void Start()
    {
        pm = GetComponentInParent<PlayerMove>();
    }

    private void FixedUpdate()
    {
        if (iframes > 0) iframes--;
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Spike" && !pm.getIsDead() && iframes == 0) {
            pm.trip();
            iframes = 50;
        } 
    }

}

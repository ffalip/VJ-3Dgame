using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideRoll : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerMove pm;
    void Start()
    {
        pm = GetComponentInParent<PlayerMove>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ColliderTop" && !pm.getIsDead()) pm.die();
    }
}

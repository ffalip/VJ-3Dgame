using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deleteSection : MonoBehaviour
{



    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GenerateLevel genlvl = FindObjectOfType<GenerateLevel>();
            genlvl.GenerateSection();
            Invoke("DestroyObject", 0.9f);
        }
    }

    void DestroyObject()
    {
            GameObject parent = this.transform.parent.gameObject;
            Destroy(parent);
    }
}

using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(Light))]
public class LightFlicker : MonoBehaviour {
    public float min = 0.7f;
    public float max = 1.1f;
    float rand;
 
    void Start(){
        rand = Random.Range(0.0f, 65535.0f);
    }

    void Update(){
        float noise = Mathf.PerlinNoise(rand, Time.time);
        GetComponent<Light>().intensity = Mathf.Lerp(min, max, noise);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLevel : MonoBehaviour
{

    public GameObject[] section;

    public float zPos = 2.7f;
    public bool creatingSection = false;
    public int secNum;

    [SerializeField] private int minimumStraightSections = 3;
    [SerializeField] private int maximumStraightSections = 15;

    private Vector3 currentTileDirection = Vector3.forward;
    private GameObject prevSection;

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (creatingSection == false)
        {
            creatingSection = true;
            StartCoroutine(GenerateSection());
        }
    }

    IEnumerator GenerateSection()
    {
        secNum = UnityEngine.Random.Range(0, 3);
        Quaternion q = Quaternion.Euler(0, 90, 0);
        Instantiate(section[secNum], new Vector3(0, 0, zPos), q);
        zPos += 2.7f;
        yield return new WaitForSeconds(0.8f);
        creatingSection = false;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLevel : MonoBehaviour
{

    public GameObject[] section;

    public float offset = 2.7f;
    public bool creatingSection = false;
    public int secNum;

    [SerializeField] private int minimumStraightSections = 3;
    [SerializeField] private int maximumStraightSections = 15;

    private Vector3 currentTileDirection = Vector3.forward;
    private Vector3 currentTilePosition = Vector3.zero;
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
        if (Input.GetKeyDown("a"))
        {
            if (currentTileDirection == Vector3.forward)
            {
                currentTileDirection = Vector3.left;
            }
            else if (currentTileDirection == Vector3.left)
            {
                currentTileDirection = Vector3.back;
            }
            else if (currentTileDirection == Vector3.back)
            {
                currentTileDirection = Vector3.right;
            }
            else
            {
                currentTileDirection = Vector3.forward;
            }
        }
        else if (Input.GetKeyDown("d"))
        {
            if (currentTileDirection == Vector3.forward)
            {
                currentTileDirection = Vector3.right;
            }
            else if (currentTileDirection == Vector3.left)
            {
                currentTileDirection = Vector3.back;
            }
            else if (currentTileDirection == Vector3.back)
            {
                currentTileDirection = Vector3.left;
            }
            else
            {
                currentTileDirection = Vector3.forward;
            }
        }
    }

    IEnumerator GenerateSection()
    {
        secNum = UnityEngine.Random.Range(0, 3);
        Quaternion q = Quaternion.Euler(0, 90, 0);
        Instantiate(section[secNum], currentTilePosition + currentTileDirection*2.7f, q * Quaternion.Euler(currentTileDirection));
        currentTilePosition += currentTileDirection * 2.7f;
        yield return new WaitForSeconds(0.8f);
        creatingSection = false;
    }
}

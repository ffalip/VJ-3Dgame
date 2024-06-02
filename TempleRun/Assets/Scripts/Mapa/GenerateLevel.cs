using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GenerateLevel : MonoBehaviour
{

    public List<GameObject> section;
    public List<GameObject> generatedSections;

    public float offset = 2.7f;
    public bool creatingSection = false;
    public int secNum;

    [SerializeField] private int currentStraight = 0;

    private Vector3 currentTileDirection = Vector3.forward;
    private Vector3 currentTilePosition = Vector3.zero;
    private bool playerIsDead = false;
    private bool playerIsTrip = false;
    private GameObject prevSection;

    public int sectionNum;
    public int turnSectionNum;
    private void Start()
    {
        for (int i = 0; i < 15; ++i) GenerateIni(i);
    }

    // Update is called once per frame
    void Update()
    {
        if (creatingSection == false  && !playerIsDead)
        {
            //creatingSection = true;
            //StartCoroutine(GenerateSection());
            /*
            if (generatedSections.Count >= 20)
            {
                
                Destroy(generatedSections[0]);
                generatedSections.Remove(generatedSections[0]);
            }
            */
        }
    }

    public void GenerateSection()
    {
        if (currentStraight < 7)
        {
            secNum = UnityEngine.Random.Range(0, sectionNum);
        }
        else if (currentStraight >= 7 && currentStraight < 15)
        {
            secNum = UnityEngine.Random.Range(0, turnSectionNum);
            if (secNum == sectionNum || secNum == turnSectionNum - 1) currentStraight = 0;
        }
        else if (currentStraight >= 15)
        {
            currentStraight = 0;
            secNum = UnityEngine.Random.Range(sectionNum, turnSectionNum);
        }
        ++currentStraight;

        Quaternion q;
        if (currentTileDirection == Vector3.forward)
        {
            q = Quaternion.Euler(0, 90, 0);
            if (secNum == sectionNum) q = Quaternion.Euler(0, 0, 0);
        }
        else if (currentTileDirection == Vector3.right)
        {
            q = Quaternion.Euler(0, 180, 0);
            if (secNum == sectionNum) q = Quaternion.Euler(0, 90, 0);
        }
        else if (currentTileDirection == Vector3.left)
        {
            q = Quaternion.Euler(0, 0, 0);
            if (secNum == sectionNum) q = Quaternion.Euler(0, 270, 0);
        }
        else
        {
            q = Quaternion.Euler(0, 270, 0);
            if (secNum == sectionNum) q = Quaternion.Euler(0, 180, 0);
        }

        
        generatedSections.Add(GameObject.Instantiate(section[secNum], currentTilePosition + currentTileDirection * 2.7f, q));
        currentTilePosition += currentTileDirection * 2.7f;

        if (secNum == sectionNum)
        {
            if (currentTileDirection == Vector3.forward)
            {
                currentTileDirection = Vector3.right;
            }
            else if (currentTileDirection == Vector3.left)
            {
                currentTileDirection = Vector3.forward;
            }
            else if (currentTileDirection == Vector3.back)
            {
                currentTileDirection = Vector3.left;
            }
            else
            {
                currentTileDirection = Vector3.back;
            }
        }

        if (secNum == sectionNum+1)
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
    
        creatingSection = false;
    }

    void GenerateIni(int i)
    {
        if (currentStraight < 7)
        {
            if (i == 0) secNum = 0;
            else if (i > 0 && i < 6) secNum =  UnityEngine.Random.Range(0, 5);
            else secNum = UnityEngine.Random.Range(0, sectionNum);
            
        }
        else if (currentStraight >= 7  && currentStraight < 11)
        {
            secNum = UnityEngine.Random.Range(0, turnSectionNum);
            if (secNum == sectionNum || secNum == turnSectionNum-1) currentStraight = 0;
        }
        else if (currentStraight >= 11)
        {
            currentStraight = 0;
            secNum = UnityEngine.Random.Range(sectionNum, turnSectionNum);
        }
        ++currentStraight;
        Quaternion q;
        if (currentTileDirection == Vector3.forward)
        {
            q = Quaternion.Euler(0, 90, 0);
            if (secNum == sectionNum) q = Quaternion.Euler(0, 0, 0);
        }
        else if (currentTileDirection == Vector3.right)
        {
            q = Quaternion.Euler(0, 180, 0);
            if (secNum == sectionNum) q = Quaternion.Euler(0, 90, 0);
        }
        else if (currentTileDirection == Vector3.left)
        {
            q = Quaternion.Euler(0, 0, 0);
            if (secNum == sectionNum) q = Quaternion.Euler(0, 270, 0);
        }
        else
        {
            q = Quaternion.Euler(0, 270, 0);
            if (secNum == sectionNum) q = Quaternion.Euler(0, 180, 0);
        }


        generatedSections.Add(GameObject.Instantiate(section[secNum], currentTilePosition + currentTileDirection * 2.7f, q));
        currentTilePosition += currentTileDirection * 2.7f;

        if (secNum == sectionNum)
        {
            if (currentTileDirection == Vector3.forward)
            {
                currentTileDirection = Vector3.right;
            }
            else if (currentTileDirection == Vector3.left)
            {
                currentTileDirection = Vector3.forward;
            }
            else if (currentTileDirection == Vector3.back)
            {
                currentTileDirection = Vector3.left;
            }
            else
            {
                currentTileDirection = Vector3.back;
            }
        }

        if (secNum == sectionNum+1)
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
    }
    
    public void setPlayerIsDead()
    {
        playerIsDead = true;
    }
    public void changeIsTrip()
    {
        playerIsTrip = !playerIsTrip;
    }
    public void destroyLastTile()
    {
        Destroy(generatedSections[0]);
        generatedSections.Remove(generatedSections[0]);
    }
}

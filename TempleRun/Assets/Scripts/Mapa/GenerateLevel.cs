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

    
    //[SerializeField] private int minimumStraightSections = 5;
    //[SerializeField] private int maximumStraightSections = 15;
    [SerializeField] private int currentStraight = 0;

    private Vector3 currentTileDirection = Vector3.forward;
    private Vector3 currentTilePosition = Vector3.zero;
    private bool playerIsDead = false;
    private bool playerIsTrip = false;
    private GameObject prevSection;

    private void Start()
    {
        for (int i = 0; i < 15; ++i) GenerateIni();
    }

    // Update is called once per frame
    void Update()
    {
        if (creatingSection == false  && !playerIsDead)
        {
            creatingSection = true;
            StartCoroutine(GenerateSection());
            if (generatedSections.Count >= 20)
            {
                
                Destroy(generatedSections[0]);
                generatedSections.Remove(generatedSections[0]);
            }
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
    }

    IEnumerator GenerateSection()
    {
        if (currentStraight < 5)
        {
            secNum = UnityEngine.Random.Range(0, 3);
        }
        else if (currentStraight >= 7)
        {
            currentStraight = 0;
            secNum = UnityEngine.Random.Range(3, 5);
        }
        ++currentStraight;

        Quaternion q;
        if (currentTileDirection == Vector3.forward)
        {
            q = Quaternion.Euler(0, 90, 0);
            if (secNum == 3) q = Quaternion.Euler(0, 0, 0);
        }
        else if (currentTileDirection == Vector3.right)
        {
            q = Quaternion.Euler(0, 180, 0);
            if (secNum == 3) q = Quaternion.Euler(0, 90, 0);
        }
        else if (currentTileDirection == Vector3.left)
        {
            q = Quaternion.Euler(0, 0, 0);
            if (secNum == 3) q = Quaternion.Euler(0, 270, 0);
        }
        else
        {
            q = Quaternion.Euler(0, 270, 0);
            if (secNum == 3) q = Quaternion.Euler(0, 180, 0);
        }

        
        generatedSections.Add(GameObject.Instantiate(section[secNum], currentTilePosition + currentTileDirection * 2.7f, q));
        currentTilePosition += currentTileDirection * 2.7f;

        if (secNum == 3)
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

        if (secNum == 4)
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
        
        
        if (!playerIsTrip) yield return new WaitForSeconds(1f);
        else yield return new WaitForSeconds(2f);
        creatingSection = false;
    }

    void GenerateIni()
    {
        if (currentStraight < 5)
        {
            secNum = UnityEngine.Random.Range(0, 3);
        }
        else if (currentStraight >= 7)
        {
            currentStraight = 0;
            secNum = UnityEngine.Random.Range(3, 5);
        }
        ++currentStraight;

        Quaternion q;
        if (currentTileDirection == Vector3.forward)
        {
            q = Quaternion.Euler(0, 90, 0);
            if (secNum == 3) q = Quaternion.Euler(0, 0, 0);
        }
        else if (currentTileDirection == Vector3.right)
        {
            q = Quaternion.Euler(0, 180, 0);
            if (secNum == 3) q = Quaternion.Euler(0, 90, 0);
        }
        else if (currentTileDirection == Vector3.left)
        {
            q = Quaternion.Euler(0, 0, 0);
            if (secNum == 3) q = Quaternion.Euler(0, 270, 0);
        }
        else
        {
            q = Quaternion.Euler(0, 270, 0);
            if (secNum == 3) q = Quaternion.Euler(0, 180, 0);
        }


        generatedSections.Add(GameObject.Instantiate(section[secNum], currentTilePosition + currentTileDirection * 2.7f, q));
        currentTilePosition += currentTileDirection * 2.7f;

        if (secNum == 3)
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

        if (secNum == 4)
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
}

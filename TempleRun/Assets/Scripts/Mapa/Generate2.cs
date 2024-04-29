using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generate2 : MonoBehaviour
{
    [SerializeField]
    private int tileStartCount = 10;
    [SerializeField]
    private int minimumStraightTiles = 3;
    [SerializeField]
    private int maximumStraightTiles = 15;
    [SerializeField]
    private GameObject startingTile;
    [SerializeField]
    private List<GameObject> turnTiles;

    private Vector3 currentTileLocation = Vector3.zero;
    private Vector3 currentTileDirection = Vector3.forward;
    private GameObject prevTile;
    private List<GameObject> currentTiles;

    void Start()
    {
        currentTiles = new List<GameObject>();
        Random.InitState(System.DateTime.Now.Millisecond);

        for (int i = 0; i < tileStartCount; ++i)
        {
            SpawnTile(startingTile.GetComponent<IdSection>());
        }
        //SpawnTile(SelectRandomGameObjectFromList(turnTiles).GetComponent<IdSection>());
        SpawnTile(turnTiles[1].GetComponent<IdSection>());
        AddNewDirection(Vector3.right);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnTile(IdSection id)
    {
        Quaternion q = id.gameObject.transform.rotation * Quaternion.LookRotation(currentTileDirection, Vector3.up);
        prevTile = GameObject.Instantiate(id.gameObject, currentTileLocation, q);
        currentTiles.Add(prevTile); 
        if(id.id == 0)
        {
            currentTileLocation += Vector3.Scale(new Vector3(2.7f, 2.7f, 2.7f), currentTileDirection);
        }
    }

    private void DeletePreviousTiles()
    {
        
    }

    public void AddNewDirection(Vector3 direction)
    {
        currentTileDirection = direction;
        DeletePreviousTiles();

        Vector3 tilePlacementScale;

        tilePlacementScale = Vector3.Scale(new Vector3(1.8f,2.7f,2.7f), currentTileDirection);
        tilePlacementScale += Vector3.forward * 0.9f;
        currentTileLocation += tilePlacementScale;
        int currentPathLength = Random.Range(minimumStraightTiles, maximumStraightTiles);
        for (int i = 0; i < currentPathLength; ++i)
        {
            SpawnTile(startingTile.GetComponent<IdSection>());
        }
        SpawnTile(SelectRandomGameObjectFromList(turnTiles).GetComponent<IdSection>());
    }

    private GameObject SelectRandomGameObjectFromList(List<GameObject> list) {
        if (list.Count == 0) return null;
        return list[Random.Range(0, list.Count)];
    }
}

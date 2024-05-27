using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideElements : MonoBehaviour
{
    public Transform target;
    public LayerMask obstacleLayer;
    public LayerMask defaultLayer;
    public string hiddenLayerName = "Hidden";

    private void Update()
    {
        HideObjectsBetweenCameraAndTarget();
    }

    void HideObjectsBetweenCameraAndTarget()
    {
        Vector3 direction = new Vector3( target.position.x, target.position.y+1, target.position.z )- transform.position;
        float distance = direction.magnitude;
        RaycastHit[] hits = Physics.RaycastAll(transform.position, direction, distance, obstacleLayer);

        foreach (RaycastHit hit in hits)
        {
            GameObject hitObject = hit.collider.gameObject;
            hitObject.layer = LayerMask.NameToLayer(hiddenLayerName);
        }
    }
}
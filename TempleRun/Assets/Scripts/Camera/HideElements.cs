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
        // Calculate the direction and distance between the camera and the target
        Vector3 direction = new Vector3(target.position.x, target.position.y + 1, target.position.z) - transform.position;
        float distance = direction.magnitude;

        // Perform a raycast to detect all objects in the path
        RaycastHit[] hits = Physics.RaycastAll(transform.position, direction, distance, obstacleLayer);

        foreach (RaycastHit hit in hits)
        {
            GameObject hitObject = hit.collider.gameObject;

            // Check if the object has the specified tag
            if (hitObject.tag == "EliminarCamera")
            {
                // Change the layer of the hit object
                hitObject.layer = LayerMask.NameToLayer(hiddenLayerName);

                // Iterate through all child transforms and change their layers as well
                foreach (Transform child in hitObject.transform)
                {
                    child.gameObject.layer = LayerMask.NameToLayer(hiddenLayerName);
                }
            }
        }
    }
}
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARObjectPlacer : MonoBehaviour
{
    public GameObject furniturePrefab;
    public ARRaycastManager raycastManager;
    public ARPlaneManager planeManager;

    private GameObject spawnedObject;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector2 touchPos = Input.GetTouch(0).position;

            if (raycastManager.Raycast(touchPos, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;

                if (spawnedObject == null)
                {
                    spawnedObject = Instantiate(furniturePrefab, hitPose.position, hitPose.rotation);
                }
                else
                {
                    // Select furniture again for editing
                    float dist = Vector3.Distance(touchPos, Camera.main.WorldToScreenPoint(spawnedObject.transform.position));
                    if (dist < 150f) // threshold to detect tap
                    {
                        spawnedObject.GetComponent<FurnitureCustomizer>()?.ShowEditUI();
                    }
                }
            }
        }
    }
}

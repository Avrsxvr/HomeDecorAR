using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class FurnitureInteraction : MonoBehaviour
{
    private Camera arCamera;
    private bool isSelected = false;
    private Vector2 lastTouchPos;
    private float initialDistance;
    private Vector3 initialScale;

    [SerializeField] private ARRaycastManager raycastManager;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Start()
    {
        arCamera = Camera.main;
    }

    void Update()
    {
#if UNITY_EDITOR
        HandleMouseInput();
#else
        HandleTouchInput();
#endif
    }

    void HandleTouchInput()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = arCamera.ScreenPointToRay(touch.position);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.transform == transform)
                    {
                        isSelected = true;
                        lastTouchPos = touch.position;
                    }
                    else
                    {
                        isSelected = false;
                    }
                }
            }
            else if (touch.phase == TouchPhase.Moved && isSelected)
            {
                // Drag-move logic
                Vector2 touchPos = touch.position;
                if (raycastManager.Raycast(touchPos, hits, TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = hits[0].pose;
                    transform.position = hitPose.position;
                }
            }
        }
        else if (Input.touchCount == 2 && isSelected)
        {
            // Handle pinch scaling and rotation
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            float currentDistance = Vector2.Distance(touch0.position, touch1.position);

            if (touch0.phase == TouchPhase.Began || touch1.phase == TouchPhase.Began)
            {
                initialDistance = currentDistance;
                initialScale = transform.localScale;
            }
            else
            {
                // Scale
                if (Mathf.Abs(initialDistance) > 0.01f)
                {
                    float scaleFactor = currentDistance / initialDistance;
                    transform.localScale = initialScale * scaleFactor;
                }

                // Rotate
                Vector2 prevDir = (touch0.position - touch0.deltaPosition) - (touch1.position - touch1.deltaPosition);
                Vector2 currentDir = touch0.position - touch1.position;
                float angle = Vector2.SignedAngle(prevDir, currentDir);
                transform.Rotate(Vector3.up, -angle); // Negative to match gesture direction
            }
        }
    }

    void HandleMouseInput()
    {
        // Selection
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = arCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform == transform)
                {
                    isSelected = true;
                }
                else
                {
                    isSelected = false;
                }
            }
        }

        // Move on plane
        if (Input.GetMouseButton(0) && isSelected)
        {
            if (raycastManager.Raycast(Input.mousePosition, hits, TrackableType.PlaneWithinPolygon))
            {
                transform.position = hits[0].pose.position;
            }
        }

        // Scale using scroll
        if (isSelected)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (Mathf.Abs(scroll) > 0.01f)
            {
                transform.localScale += Vector3.one * scroll;
            }
        }

        // Optional: Rotate using right click + drag
        if (Input.GetMouseButton(1) && isSelected)
        {
            float rotX = Input.GetAxis("Mouse X") * 5f;
            transform.Rotate(Vector3.up, -rotX);
        }
    }
}

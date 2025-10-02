using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

public class PlaneTrackingToggle : MonoBehaviour
{
    public ARPlaneManager planeManager;

    public GameObject stopTrackingButton;
    public GameObject startTrackingButton;

    void Start()
    {
        // Initially tracking is ON, so show Stop button and hide Start button
        stopTrackingButton.SetActive(true);
        startTrackingButton.SetActive(false);
    }

    public void StopPlaneTracking()
    {
        if (planeManager != null)
        {
            // Disable new plane detection
            planeManager.enabled = false;

            // Keep existing planes visible (don't disable them)
            foreach (var plane in planeManager.trackables)
            {
                plane.gameObject.SetActive(true);
            }

            stopTrackingButton.SetActive(false);
            startTrackingButton.SetActive(true);
        }
    }

    public void StartPlaneTracking()
    {
        if (planeManager != null)
        {
            // Enable new plane detection
            planeManager.enabled = true;

            stopTrackingButton.SetActive(true);
            startTrackingButton.SetActive(false);
        }
    }
}

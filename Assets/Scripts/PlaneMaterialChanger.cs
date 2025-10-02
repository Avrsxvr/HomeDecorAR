using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PlaneMaterialChanger : MonoBehaviour
{
    public ARPlaneManager planeManager;
    public List<Material> planeMaterials;

    // Call this method from your UI button
    public void ChangePlaneMaterial(int materialIndex)
    {
        if (materialIndex < 0 || materialIndex >= planeMaterials.Count)
        {
            Debug.LogWarning("Invalid material index!");
            return;
        }

        Material selectedMaterial = planeMaterials[materialIndex];

        foreach (ARPlane plane in planeManager.trackables)
        {
            MeshRenderer meshRenderer = plane.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                meshRenderer.material = selectedMaterial;
            }
        }
    }
}

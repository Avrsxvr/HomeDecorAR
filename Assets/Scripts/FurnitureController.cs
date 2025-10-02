using UnityEngine;

public class FurnitureController : MonoBehaviour
{
    public Material[] availableMaterials; // Assign materials in inspector
    private Renderer meshRenderer;

    void Start()
    {
        meshRenderer = GetComponentInChildren<Renderer>();
    }

    public void ApplyMaterial(int index)
    {
        if (index >= 0 && index < availableMaterials.Length)
        {
            meshRenderer.material = availableMaterials[index];
        }
    }

    public Material[] GetMaterials()
    {
        return availableMaterials;
    }
}

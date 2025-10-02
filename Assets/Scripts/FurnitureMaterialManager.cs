using UnityEngine;

[System.Serializable]
public class MaterialInfo
{
    public Material material;
    public Sprite previewImage;
    public string label;
}

public class FurnitureMaterialManager : MonoBehaviour
{
    public MaterialInfo[] availableMaterials;

    private Renderer rend;

    void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    public void ApplyMaterial(int index)
    {
        if (rend != null && index >= 0 && index < availableMaterials.Length)
        {
            rend.material = availableMaterials[index].material;
        }
    }

    public MaterialInfo[] GetMaterials()
    {
        return availableMaterials;
    }
}

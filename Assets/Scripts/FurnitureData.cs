using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class FurnitureMaterial
{
    public string materialName;
    public Material material;
    public Sprite previewImage;
}

public class FurnitureData : MonoBehaviour
{
    public FurnitureMaterial[] materials; // Each furniture's unique materials
}

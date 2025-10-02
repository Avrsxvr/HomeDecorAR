using UnityEngine;
using UnityEngine.UI;

public class MaterialUIManager : MonoBehaviour
{
    public GameObject materialButtonPrefab;   // A prefab with a UI Button + Text
    public Transform materialButtonParent;    // The content container inside the material panel

    private FurnitureController currentFurniture;

    public void SetSelectedFurniture(FurnitureController furniture)
    {
        currentFurniture = furniture;
    }

    public void ShowMaterialOptions()
    {
        // Remove old buttons
        foreach (Transform child in materialButtonParent)
        {
            Destroy(child.gameObject);
        }

        if (currentFurniture == null) return;

        Material[] materials = currentFurniture.GetMaterials();

        for (int i = 0; i < materials.Length; i++)
        {
            int index = i; // capture index for closure
            GameObject buttonObj = Instantiate(materialButtonPrefab, materialButtonParent);
            buttonObj.GetComponentInChildren<Text>().text = "Material " + (i + 1);
            buttonObj.GetComponent<Button>().onClick.AddListener(() => {
                currentFurniture.ApplyMaterial(index);
            });
        }
    }
}

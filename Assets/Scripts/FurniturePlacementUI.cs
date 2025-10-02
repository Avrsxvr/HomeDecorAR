using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Interaction.Toolkit.AR; // Add this namespace for ARPlacementInteractable

public class FurniturePlacementUI : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject mainPlaceButton;
    public GameObject furnitureMenuPanel;

    [Header("AR Placement")]
    public ARPlacementInteractable arPlacementInteractable;

    [Header("Furniture Prefabs")]
    public GameObject[] furniturePrefabs; // Chair, Table, etc.

    void Start()
    {
        furnitureMenuPanel.SetActive(false);
        arPlacementInteractable.placementPrefab = null;

        mainPlaceButton.SetActive(true);
        mainPlaceButton.GetComponent<Button>().onClick.AddListener(OpenFurnitureMenu);
    }

    public void OpenFurnitureMenu()
    {
        furnitureMenuPanel.SetActive(true);
        mainPlaceButton.SetActive(false);
    }

    public void SelectFurniture(int index)
    {
        if (index < 0 || index >= furniturePrefabs.Length)
        {
            Debug.LogWarning("Invalid furniture index.");
            return;
        }

        // Set the selected prefab to be placed
        arPlacementInteractable.placementPrefab = furniturePrefabs[index];

        // Close the menu
        furnitureMenuPanel.SetActive(false);
        mainPlaceButton.SetActive(true);

        Debug.Log("Selected furniture: " + furniturePrefabs[index].name);
    }
}

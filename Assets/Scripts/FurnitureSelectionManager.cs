using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class FurnitureSelectionManager : MonoBehaviour
{
    public static FurnitureSelectionManager instance;

    [Header("UI References")]
    public GameObject editButton;         // Assigned in Inspector
    public GameObject materialPanel;      // Assigned in Inspector
    public Button[] materialButtons;      // Assigned in Inspector (4 buttons)

    private GameObject selectedFurniture;
    private FurnitureData selectedData;

    void Awake()
    {
        instance = this;

        // Hide edit button and panel at start
        editButton.SetActive(false);
        materialPanel.SetActive(false);
    }

    // Called when a furniture is selected (by XR Selection Interactable)
    public void OnFurnitureSelected(GameObject furniture)
    {
        if (selectedFurniture == furniture) return;

        selectedFurniture = furniture;
        selectedData = furniture.GetComponent<FurnitureData>();

        // Show edit button
        editButton.SetActive(true);

        // Hide material panel until edit is tapped
        materialPanel.SetActive(false);

        // Update material button previews (image and text)
        UpdateMaterialButtonPreviews();
    }

    private void UpdateMaterialButtonPreviews()
    {
        if (selectedData == null) return;

        for (int i = 0; i < materialButtons.Length; i++)
        {
            if (i >= selectedData.materials.Length) continue;

            FurnitureMaterial mat = selectedData.materials[i];

            // Automatically find Image and TMP_Text inside the button
            Image img = materialButtons[i].GetComponentInChildren<Image>();
            TMP_Text text = materialButtons[i].GetComponentInChildren<TMP_Text>();

            if (img != null) img.sprite = mat.previewImage;
            if (text != null) text.text = mat.materialName;

            int index = i;
            materialButtons[i].onClick.RemoveAllListeners();
            materialButtons[i].onClick.AddListener(() => ApplyMaterial(index));
        }
    }

    // Called when Edit Button is tapped
    public void OnEditButtonClicked()
    {
        materialPanel.SetActive(true);
    }

    // Applies material to selected furniture (child model)
    public void ApplyMaterial(int index)
    {
        if (selectedFurniture == null || selectedData == null) return;

        // ✅ FIX: target Renderer on child object (like your Couch)
        Renderer rend = selectedFurniture.GetComponentInChildren<Renderer>();

        if (rend != null && index < selectedData.materials.Length)
        {
            rend.material = selectedData.materials[index].material;
            Debug.Log("✅ Material applied: " + selectedData.materials[index].material.name);
        }
        else
        {
            Debug.LogWarning("⚠️ Renderer not found or index out of range.");
        }
    }
}
// This script manages the furniture selection and material application in the AR environment.
// It listens for selection events, updates the UI with material previews, and applies materials to the
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Required for TMP

public class MaterialPanelController : MonoBehaviour
{
    public GameObject panel;
    public Button[] materialButtons;

    private FurnitureMaterialManager currentFurniture;

    public void OpenPanel(FurnitureMaterialManager furniture)
    {
        currentFurniture = furniture;

        var materials = furniture.GetMaterials();

        for (int i = 0; i < materialButtons.Length; i++)
        {
            if (i < materials.Length)
            {
                int index = i;

                // Get Image and TMP_Text from the Button
                Image img = materialButtons[i].GetComponentInChildren<Image>();
                TMP_Text txt = materialButtons[i].GetComponentInChildren<TMP_Text>();

                if (img != null)
                    img.sprite = materials[i].previewImage;

                if (txt != null)
                    txt.text = materials[i].label;

                // Update button click
                materialButtons[i].onClick.RemoveAllListeners();
                materialButtons[i].onClick.AddListener(() => furniture.ApplyMaterial(index));

                materialButtons[i].gameObject.SetActive(true);
            }
            else
            {
                materialButtons[i].gameObject.SetActive(false);
            }
        }

        panel.SetActive(true);
    }

    public void ClosePanel()
    {
        panel.SetActive(false);
    }
}
// This script manages the material selection panel for furniture.
// It populates the buttons with material previews and labels,
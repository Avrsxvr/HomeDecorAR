using UnityEngine;
using UnityEngine.UI;

public class ARSelectionHandler : MonoBehaviour
{
    public static ARSelectionHandler Instance;

    public Button editButton;
    public GameObject materialPanel;
    public MaterialUIManager materialUIManager;

    private FurnitureController selectedFurniture;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        editButton.gameObject.SetActive(false);
        materialPanel.SetActive(false);
        editButton.onClick.AddListener(OnEditButtonClicked);
    }

    public void OnObjectSelected(GameObject selectedObj)
    {
        FurnitureController newFurniture = selectedObj.GetComponent<FurnitureController>();

        if (newFurniture != null && newFurniture != selectedFurniture)
        {
            selectedFurniture = newFurniture;
            materialUIManager.SetSelectedFurniture(selectedFurniture);
            editButton.gameObject.SetActive(true);
        }
    }

    void OnEditButtonClicked()
    {
        if (selectedFurniture != null)
        {
            materialPanel.SetActive(true);
            materialUIManager.ShowMaterialOptions();
        }
    }
}

using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FurnitureSelectable : MonoBehaviour
{
    private XRBaseInteractable interactable;

    void Awake()
    {
        interactable = GetComponent<XRBaseInteractable>();
        interactable.selectEntered.AddListener(OnSelect);
    }

    private void OnSelect(SelectEnterEventArgs args)
    {
        FurnitureSelectionManager.instance.OnFurnitureSelected(gameObject);
    }
}
// This script allows furniture objects to be selectable in the AR environment.
// It uses XR Interaction Toolkit to handle selection events and notifies the FurnitureSelectionManager when selected
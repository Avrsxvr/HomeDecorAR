using UnityEngine;
using UnityEngine.UI;

public class FurnitureCustomizer : MonoBehaviour
{
    public GameObject editPanel;
    public Material[] materials;
    private int currentMaterialIndex = 0;

    void Start()
    {
        editPanel.SetActive(false);
    }

    public void ShowEditUI()
    {
        editPanel.SetActive(true);
    }

    public void Move(Vector3 direction)
    {
        transform.position += direction;
    }

    public void Rotate()
    {
        transform.Rotate(0, 45f, 0);
    }

    public void ScaleUp()
    {
        transform.localScale *= 1.1f;
    }

    public void ScaleDown()
    {
        transform.localScale *= 0.9f;
    }

    public void ChangeMaterial()
    {
        currentMaterialIndex = (currentMaterialIndex + 1) % materials.Length;
        GetComponent<MeshRenderer>().material = materials[currentMaterialIndex];
    }

    public void CloseEditUI()
    {
        editPanel.SetActive(false);
    }
}

using UnityEngine;

public class Inventory_Toggle : MonoBehaviour
{
    public GameObject inventoryMenu;
    public KeyCode toggleKey = KeyCode.KeypadEnter;

    private bool isOpen = false;

    private void Start()
    {
        if (inventoryMenu != null)
        {
            inventoryMenu.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        isOpen = !isOpen;

        if (inventoryMenu != null)
        {
            inventoryMenu.SetActive(isOpen);
        }
    }
}

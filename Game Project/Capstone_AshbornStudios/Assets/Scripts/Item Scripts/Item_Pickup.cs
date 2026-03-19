using UnityEngine;
using UnityEditor;

public class Item_Pickup : MonoBehaviour
{
    [Header("Item Data")]
    public string itemName = "Item Name";
    public Sprite itemIcon;
    public int amount = 1;

    private void Reset()
    {
        Collider col = GetComponent<Collider>();
        if (col != null)
            col.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Inventory.Instance == null)
            {
                Debug.LogWarning("Inventory instance not found!");
                return;
            }

            // Create item with correct constructor
            Item newItem = new Item(itemName, itemIcon, amount);

            // Add to inventory
            Inventory.Instance.AddItem(newItem);

            // Destroy pickup object
            Destroy(gameObject);
        }
    }
}
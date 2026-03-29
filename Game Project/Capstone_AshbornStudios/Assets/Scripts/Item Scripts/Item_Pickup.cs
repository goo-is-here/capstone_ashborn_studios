using UnityEngine;

public class Item_Pickup : MonoBehaviour
{
    public string itemName = "Item Name";
    public string itemdescription = "Item Description";
    public Sprite itemIcon;
    public int amount = 1;

    [Header("What to destroy after pickup")]
    public GameObject objectToDestroy;

    private bool pickedUp = false;

    private void Awake()
    {
        if (objectToDestroy == null)
            objectToDestroy = gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (pickedUp) return;
        if (!other.CompareTag("Player")) return;

        if (Inventory.Instance == null)
        {
            Debug.LogWarning("Inventory instance not found!");
            return;
        }

        pickedUp = true;

        Item newItem = new Item(itemName, itemdescription, itemIcon, amount);
        Inventory.Instance.AddItem(newItem);

        Destroy(objectToDestroy);
    }
}
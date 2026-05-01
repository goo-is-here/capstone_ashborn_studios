using UnityEngine;

public class Item_Pickup : MonoBehaviour
{
    public bool pickUp = true;
    public string itemName = "Item Name";
    public string itemdescription = "Item Description";
    public Sprite itemIcon;
    public int amount = 1;
    public ItemEnum enu;

    [Header("Prefab used when dropping this item again")]
    public GameObject worldPrefab;

    [Header("Scene object to destroy after pickup")]
    public GameObject objectToDestroy;

    private bool pickedUp = false;

    private void Awake()
    {
        if (objectToDestroy == null)
            objectToDestroy = gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        print("here");
        if (!pickUp) return;
        if (pickedUp) return;
        if (!other.CompareTag("Player")) return;

        if (Inventory.Instance == null)
        {
            Debug.LogWarning("Inventory instance not found!");
            return;
        }

        if (worldPrefab == null)
        {
            Debug.LogWarning("World prefab is not assigned for: " + itemName);
            return;
        }

        pickedUp = true;

        Item newItem = new Item(
            itemName,
            itemdescription,
            itemIcon,
            amount,
            enu,
            worldPrefab
        );

        Inventory.Instance.AddItem(newItem);

        Destroy(objectToDestroy);
    }
}
using UnityEngine;

public class Food_Script : MonoBehaviour
{
    [Header("Item Data")]
    public string itemName = "Food";
    public string description = "Restores hunger";
    public Sprite icon;
    public int count = 1;
    public ItemEnum enu;
    public float hungerRestoreAmount = 25f;
    public GameObject worldPrefab;

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

        Pickup();
    }

    public void Pickup()
    {
        if (pickedUp) return;

        if (Inventory.Instance == null)
        {
            Debug.LogWarning("Inventory.Instance is null.");
            return;
        }

        pickedUp = true;

        Food_Item food = new Food_Item(
            itemName,
            description,
            icon,
            count,
            enu,
            worldPrefab,
            hungerRestoreAmount
        );

        Inventory.Instance.AddItem(food);

        Destroy(objectToDestroy);
    }
}
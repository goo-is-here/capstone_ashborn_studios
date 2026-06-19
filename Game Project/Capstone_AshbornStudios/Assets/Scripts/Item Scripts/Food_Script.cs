using UnityEngine;

public class Food_Script : MonoBehaviour
{
    //food item variables
    [Header("Item Data")]
    public string itemName = "Food";
    public string description = "Restores hunger";
    public Sprite icon;
    public int count = 1;
    public ItemEnum enu;
    public float hungerRestoreAmount = 25f;
    public GameObject worldPrefab;

    //TODO BAD replace
    [Header("What to destroy after pickup")]
    public GameObject objectToDestroy;

    private bool pickedUp = false;
    //grabs game object, bad replace
    private void Awake()
    {
        if (objectToDestroy == null)
            objectToDestroy = gameObject;
    }
    //checks if player entered object
    private void OnTriggerEnter(Collider other)
    {
        if (pickedUp) return;
        if (!other.CompareTag("Player")) return;
        //picks up object
        Pickup();
    }
    //picks up object
    public void Pickup()
    {
        if (pickedUp) return;
        //makes sure inventory is not null
        if (Inventory.Instance == null)
        {
            Debug.LogWarning("Inventory.Instance is null.");
            return;
        }

        pickedUp = true;
        //makes a new food item to add it to the inventory
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
        //detroys object
        Destroy(objectToDestroy);
    }
}
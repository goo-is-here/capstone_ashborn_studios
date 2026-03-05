using UnityEngine;
using UnityEditor;

public class Item_Pickup : MonoBehaviour
{
    public Item item = new Item("Item Name", 1);
    

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Inventory.Instance.AddItem(item);
            Destroy(gameObject);
        }
    }
}
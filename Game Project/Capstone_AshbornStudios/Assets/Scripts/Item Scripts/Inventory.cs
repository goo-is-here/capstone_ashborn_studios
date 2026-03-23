using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    public List<Item> Items = new List<Item>();
    public InventoryUI inventoryUI;

    void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public void AddItem(Item itemToAdd)
    {
        int remaining = itemToAdd.count;

        
        foreach (Item item in Items)
        {
            if (item.name == itemToAdd.name && item.count < 99)
            {
                int spaceLeft = 99 - item.count;

                if (remaining <= spaceLeft)
                {
                    item.count += remaining;
                    remaining = 0;
                    break;
                }
                else
                {
                    item.count = 99;
                    remaining -= spaceLeft;
                }
            }
        }

        
        while (remaining > 0)
        {
            int stackAmount = Mathf.Min(remaining, 99);
            Items.Add(new Item(itemToAdd.name, itemToAdd.icon, stackAmount));
            remaining -= stackAmount;
        }

        Debug.Log(itemToAdd.count + " " + itemToAdd.name + " added to inventory.");

        if (inventoryUI != null)
        {
            inventoryUI.UpdateInventoryUI(Items);
        }
    }
}
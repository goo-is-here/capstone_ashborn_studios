using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public List<Item> Items = new List<Item>();
    public InventoryUI inventoryUI;

    [Header("Inventory Size")]
    public int slotCount = 14; // 6 hotbar + 8 backpack

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        while (Items.Count < slotCount)
        {
            Items.Add(null);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            RemoveItemAtIndex(0, 1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            RemoveItemAtIndex(1, 1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            RemoveItemAtIndex(2, 1);
        }
    }

    public void AddItem(Item itemToAdd)
    {
        int remaining = itemToAdd.count;

        // Fill existing stacks first
        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i] != null &&
                Items[i].name == itemToAdd.name &&
                Items[i].count < 99)
            {
                int spaceLeft = 99 - Items[i].count;
                int amountToAdd = Mathf.Min(remaining, spaceLeft);

                Items[i].count += amountToAdd;
                remaining -= amountToAdd;

                if (remaining <= 0)
                    break;
            }
        }

        // Put remaining items into empty slots
        for (int i = 0; i < Items.Count; i++)
        {
            if (remaining <= 0)
                break;

            if (Items[i] == null)
            {
                int stackAmount = Mathf.Min(remaining, 99);
                Items[i] = new Item(itemToAdd.name, itemToAdd.description, itemToAdd.icon, stackAmount, itemToAdd.worldPrefab);
                remaining -= stackAmount;
            }
        }

        if (inventoryUI != null)
        {
            inventoryUI.UpdateInventoryUI(Items);
        }

        Debug.Log(itemToAdd.count + " " + itemToAdd.name + " added to inventory.");
    }

    public Item GetItemAtIndex(int index)
    {
        if (index < 0 || index >= Items.Count)
            return null;

        return Items[index];
    }

    public void RemoveItemAtIndex(int index, int amount)
    {
        if (index < 0 || index >= Items.Count)
            return;

        if (Items[index] == null)
            return;

        Items[index].count -= amount;

        if (Items[index].count <= 0)
        {
            Items[index] = null;
        }

        if (inventoryUI != null)
        {
            inventoryUI.UpdateInventoryUI(Items);
        }
    }

    public void DropItemAtIndex(int index, int amount, Vector3 dropPosition)
    {
        if (index < 0 || index >= Items.Count)
            return;

        Item item = Items[index];
        if (item == null)
            return;

        int amountToDrop = Mathf.Min(amount, item.count);

        if (item.worldPrefab != null)
        {
            for (int i = 0; i < amountToDrop; i++)
            {
                Instantiate(item.worldPrefab, dropPosition + new Vector3(i * 0.2f, 0f, 0f), Quaternion.identity);
            }
        }

        RemoveItemAtIndex(index, amountToDrop);
    }
}
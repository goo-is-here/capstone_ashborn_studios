using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public List<Item> Items = new List<Item>();
    public InventoryUI inventoryUI;

    [Header("Inventory Size")]
    public int slotCount = 14; // 6 hotbar + 8 backpack

    [Header("Hotbar Selection")]
    public int selectedSlotIndex = 0;

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
        HandleHotbarSelection();

        if (Input.GetKeyDown(KeyCode.E))
        {
            UseSelectedItem();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RemoveSelectedItem();
        }
    }

    void HandleHotbarSelection()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) selectedSlotIndex = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2)) selectedSlotIndex = 1;
        if (Input.GetKeyDown(KeyCode.Alpha3)) selectedSlotIndex = 2;
        if (Input.GetKeyDown(KeyCode.Alpha4)) selectedSlotIndex = 3;
        if (Input.GetKeyDown(KeyCode.Alpha5)) selectedSlotIndex = 4;
        if (Input.GetKeyDown(KeyCode.Alpha6)) selectedSlotIndex = 5;
    }

    void UseSelectedItem()
    {
        Item selectedItem = GetItemAtIndex(selectedSlotIndex);

        if (selectedItem == null)
            return;

        Food_Item foodItem = selectedItem as Food_Item;

        if (foodItem != null)
        {
            HungerScript hungerScript = FindFirstObjectByType<HungerScript>();

            if (hungerScript != null)
            {
                hungerScript.RestoreHunger(foodItem.hungerRestoreAmount);
                RemoveItemAtIndex(selectedSlotIndex, 1);
                Debug.Log("Used food: " + foodItem.name);
            }
            else
            {
                Debug.LogWarning("No HungerScript found in the scene.");
            }
        }
        else
        {
            Debug.Log("Selected item is not usable.");
        }
    }

    void RemoveSelectedItem()
    {
        RemoveItemAtIndex(selectedSlotIndex, 1);
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
                Items[i] = CopyItem(itemToAdd, stackAmount);
                remaining -= stackAmount;
            }
        }

        if (inventoryUI != null)
        {
            inventoryUI.UpdateInventoryUI(Items);
        }

        Debug.Log(itemToAdd.count + " " + itemToAdd.name + " added to inventory.");
    }

    Item CopyItem(Item source, int count)
    {
        Food_Item foodSource = source as Food_Item;

        if (foodSource != null)
        {
            return new Food_Item(
                foodSource.name,
                foodSource.description,
                foodSource.icon,
                count,
                source.enu,
                foodSource.worldPrefab,
                foodSource.hungerRestoreAmount
            );
        }

        return new Item(
            source.name,
            source.description,
            source.icon,
            count,
            source.enu,
            source.worldPrefab
        );
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
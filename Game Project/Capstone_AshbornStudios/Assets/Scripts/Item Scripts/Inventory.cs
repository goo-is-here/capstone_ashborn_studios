using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemDropPrefab
{
    public ItemEnum enu;
    public GameObject prefab;
}

public class Inventory : MonoBehaviour, IDataPersistence
{
    public static Inventory Instance;

    public List<Item> Items = new List<Item>();
    public InventoryUI inventoryUI;

    [Header("Inventory Size")]
    public int slotCount = 14;

    [Header("Hotbar Selection")]
    public int selectedSlotIndex = 0;

    [Header("Drop Settings")]
    public Transform dropPoint;
    public float dropForwardForce = 2f;
    public float dropUpForce = 1f;
    public float dropDistance = 1.5f;

    [Header("Drop Prefab Lookup")]
    public List<ItemDropPrefab> dropPrefabs = new List<ItemDropPrefab>();
    public List<Item_Pickup> sprites = new List<Item_Pickup>();

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


        if (Input.GetKeyDown(KeyCode.Q))
        {
            DropSelectedItem();
        }
    }
    public void LoadData(GameData data)
    {
        foreach(Item it in data.inventory)
        {
            if(it.enu != ItemEnum.NULL)
            {
                foreach(Item_Pickup ite in sprites)
                {
                    if(it.enu == ite.enu)
                    {
                        it.icon = ite.itemIcon;
                        it.worldPrefab = ite.worldPrefab;
                    }
                }
                AddItem(it);
            }
        }
    }
    public void SaveData(ref GameData data)
    {
        data.inventory.Clear();
        foreach(Item it in Items)
        {
            if (it != null && it.enu != ItemEnum.NULL)
            {
                data.inventory.Add(it);
            }
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
        if (itemToAdd == null)
            return;

        int remaining = itemToAdd.count;

        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i] != null &&
                Items[i].enu == itemToAdd.enu &&
                Items[i].count < 99)
            {
                int spaceLeft = 99 - Items[i].count;
                int amountToAdd = Mathf.Min(remaining, spaceLeft);

                Items[i].count += amountToAdd;

                if (Items[i].worldPrefab == null)
                {
                    Items[i].worldPrefab = itemToAdd.worldPrefab;
                }

                remaining -= amountToAdd;

                if (remaining <= 0)
                    break;
            }
        }

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

        UpdateUI();

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
                foodSource.enu,
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

        UpdateUI();
    }

    GameObject GetDropPrefab(Item item)
    {
        if (item == null)
            return null;

        for (int i = 0; i < dropPrefabs.Count; i++)
        {
            if (dropPrefabs[i] != null && dropPrefabs[i].enu == item.enu)
            {
                return dropPrefabs[i].prefab;
            }
        }

        return null;
    }

    public void DropSelectedItem()
    {
        if (dropPoint == null)
        {
            Debug.LogWarning("DropPoint is not assigned.");
            return;
        }

        if (selectedSlotIndex < 0 || selectedSlotIndex >= Items.Count)
        {
            Debug.LogWarning("Selected slot index invalid: " + selectedSlotIndex);
            return;
        }

        Item itemToDrop = Items[selectedSlotIndex];

        if (itemToDrop == null)
        {
            Debug.LogWarning("Selected item is null.");
            return;
        }

        // First try to use the prefab stored inside the item data
        GameObject prefabToDrop = itemToDrop.worldPrefab;

        // If the item somehow lost its prefab, use the fallback lookup
        if (prefabToDrop == null)
        {
            prefabToDrop = GetDropPrefab(itemToDrop);
        }

        if (prefabToDrop == null)
        {
            Debug.LogWarning("No drop prefab found for: " + itemToDrop.enu);
            return;
        }

        Camera cam = Camera.main;

        Vector3 spawnPos;

        if (cam != null)
        {
            spawnPos = cam.transform.position + cam.transform.forward * 3f;
        }
        else
        {
            spawnPos = transform.position + transform.forward * 3f;
        }

        spawnPos += Vector3.up * 1f;

        GameObject droppedItem = Instantiate(
            prefabToDrop,
            spawnPos,
            Quaternion.identity
        );

        Destroy(droppedItem, 10f);

        Rigidbody rb = droppedItem.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(
                dropPoint.forward * dropForwardForce + Vector3.up * dropUpForce,
                ForceMode.Impulse
            );
        }

        itemToDrop.count--;

        if (itemToDrop.count <= 0)
        {
            Items[selectedSlotIndex] = null;
        }

        UpdateUI();

        Debug.Log("Dropped item: " + itemToDrop.enu);
    }



    void UpdateUI()
    {
        if (inventoryUI != null)
        {
            inventoryUI.UpdateInventoryUI(Items);
        }
    }
}
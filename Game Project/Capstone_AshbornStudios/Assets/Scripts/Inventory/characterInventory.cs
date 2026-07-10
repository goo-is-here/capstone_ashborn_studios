using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class characterInventory : MonoBehaviour
{
    [SerializeField] List<Item> inventoryItemList;
    [SerializeField] GameObject[] inventorySlotArray;
    [SerializeField] int numSlots = 12;
    [SerializeField] int hotBarSlots = 6;
    public GameObject slotPrefab;
    public GameObject hotBarSlotParent;
    public GameObject[] inventorySlotParent; 
    PlayerController player;
    bool showingInventory = false;
    [SerializeField] int maxItem = 99;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        //sets intial amount for array
        inventorySlotArray = new GameObject[numSlots + hotBarSlots];
        //creates hotbar slots
        for(int i = 0; i < hotBarSlots; i++)
        {
            GameObject slot = Instantiate(slotPrefab, hotBarSlotParent.transform);
            inventorySlotArray[i] = slot;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(player.canMove && Input.GetKeyDown(KeyCode.I)){
            if (!showingInventory)
            {
                displayInventory();
            }
            else
            {
                hideInventory();
            }
        }
    }
    private void displayInventory()
    {
        showingInventory = true;
        int slotNumber = 6;
        int rows = numSlots / hotBarSlots;
        for(int j = 0; j < rows; j++)
        {
            for(int i = 0; i < hotBarSlots; i++)
            {
                GameObject slot = Instantiate(slotPrefab, inventorySlotParent[j].transform);
                inventorySlotArray[slotNumber] = slot;
                slotNumber++;
            }
        }
        
    }
    private void hideInventory()
    {
        showingInventory = false;
        
        int rows = numSlots / hotBarSlots;
        int slotNumber = 6;
        for (int i = 0; i < numSlots; i++)
        {
            inventorySlotArray[slotNumber].GetComponent<InventorySlot>().emptySlot();
            Destroy(inventorySlotArray[slotNumber].gameObject);
            inventorySlotArray[slotNumber] = null;
            slotNumber++;
        }
    }
    public void addItem(Item ite)
    {
        foreach(Item obj in inventoryItemList)
        {
            if (inventoryItemList.Count == 0)
            {
                if(ite.count > maxItem)
                {
                    int overFlow = ite.count - maxItem;
                    ite.count = maxItem;
                    inventoryItemList.Add(ite);
                    Item overFlowItem = new Item(ite.name, ite.description, ite.icon, overFlow, ite.enu, ite.worldPrefab);
                    addItem(overFlowItem);
                }
                else
                {
                    inventoryItemList.Add(ite);
                }
            }
            else if(obj.enu == ite.enu && obj.count >= maxItem)
            {
                int newCount = obj.count + ite.count;
                if(newCount > maxItem)
                {
                    int overFlow = newCount - maxItem;
                    obj.count = maxItem;
                    Item overFlowItem = new Item(ite.name, ite.description, ite.icon, overFlow, ite.enu, ite.worldPrefab);
                    addItem(overFlowItem);
                }
                else
                {
                    obj.count = newCount;
                }
            }
        }
    }
    public void removeItem(Item ite, int amount)
    {
        foreach(Item obj in inventoryItemList)
        {
            if(obj.enu == ite.enu && amount > 0)
            {
                if(amount >= obj.count)
                {
                    amount -= obj.count;
                    inventoryItemList.Remove(obj);
                }
                else
                {
                    obj.count -= amount;
                    amount = -1;
                }
            }
        }
    }
}

using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class characterInventory : MonoBehaviour
{
    [SerializeField] List<Item> inventoryItemList;
    List<GameObject> inventorySlotArray;
    [SerializeField] int hotBarSlots = 6;
    [SerializeField] int numSlots = 12;
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
        inventorySlotArray = new List<GameObject>();
        //creates hotbar slots
        for(int i = 0; i < hotBarSlots; i++)
        {
            GameObject slot = Instantiate(slotPrefab, hotBarSlotParent.transform);
            inventorySlotArray.Add(slot);
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
        int rows = Mathf.CeilToInt((float)numSlots / (float)hotBarSlots);
        for(int j = 0; j < rows; j++)
        {
            for(int i = 0; i < hotBarSlots; i++)
            {
                if(slotNumber < numSlots)
                {
                    GameObject slot = Instantiate(slotPrefab, inventorySlotParent[j].transform);
                    inventorySlotArray.Add(slot);
                    slotNumber++;
                }
            }
        }
        
    }
    private void hideInventory()
    {
        showingInventory = false;
        for (int i = hotBarSlots; i < inventorySlotArray.Count; i++)
        {
            Destroy(inventorySlotArray[i].gameObject);
        }
        print(numSlots);
        inventorySlotArray.RemoveRange(hotBarSlots, numSlots-hotBarSlots);
    }
    public void addItem(Item ite)
    {
        if (inventoryItemList.Count == 0)
        {
            if (ite.count > maxItem)
            {
                int overFlow = ite.count - maxItem;
                ite.count = maxItem;
                inventoryItemList.Add(ite);
                Item overFlowItem = new Item(ite.itemName, ite.description, ite.icon, overFlow, ite.enu, ite.worldPrefab);
                addItem(overFlowItem);
            }
            else
            {
                inventoryItemList.Add(ite);
            }
        }
        else
        {
            bool added = false;
            int indexList = 0;
            while(!added && indexList < inventoryItemList.Count)
            {
                if (inventoryItemList[indexList].enu == ite.enu && inventoryItemList[indexList].count < maxItem)
                {
                    int newCount = inventoryItemList[indexList].count + ite.count;
                    if (newCount < maxItem)
                    {
                        inventoryItemList[indexList].count = newCount;
                    }
                    else
                    {
                        int overFlow = newCount - maxItem;
                        inventoryItemList[indexList].count = maxItem;
                        Item overFlowItem = new Item(ite.itemName, ite.description, ite.icon, overFlow, ite.enu, ite.worldPrefab);
                        addItem(overFlowItem);
                    }
                    added = true;
                }
                indexList++;
            }
            if(indexList < numSlots && !added)
            {
                if (ite.count > maxItem)
                {
                    int overFlow = ite.count - maxItem;
                    ite.count = maxItem;
                    inventoryItemList.Add(ite);
                    Item overFlowItem = new Item(ite.itemName, ite.description, ite.icon, overFlow, ite.enu, ite.worldPrefab);
                    addItem(overFlowItem);
                }
                else
                {
                    inventoryItemList.Add(ite);
                }
            }
            
        }
        updateDisplayedInventory();
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
        updateDisplayedInventory();
    }
    private void updateDisplayedInventory()
    {
        for(int i = 0; i < inventoryItemList.Count; i++)
        {
            inventorySlotArray[i].GetComponent<InventorySlot>().setSlot(inventoryItemList[i]);
        }
    }
}

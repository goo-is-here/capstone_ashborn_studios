using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class characterInventory : MonoBehaviour
{
    [SerializeField] List<Item> inventoryItemList;
    List<GameObject> inventorySlotArray;
    [SerializeField] int hotBarSlots = 6;
    [SerializeField] int numSlots = 12;
    [SerializeField] int numSlotsPerRow = 6;
    public GameObject slotPrefab;
    public GameObject hotBarSlotParent;
    public GameObject[] inventorySlotParent; 
    PlayerController player;
    bool showingInventory = false;
    [SerializeField] int maxItem = 99;
    int selectedSlotNum = -1;
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
        processSlot();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            dropItem();
        }
    }
    private void processSlot()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            inventorySlotArray[0].GetComponent<InventorySlot>().selectedSlot.SetActive(!inventorySlotArray[0].GetComponent<InventorySlot>().selectedSlot.activeSelf);
            inventorySlotArray[1].GetComponent<InventorySlot>().selectedSlot.SetActive(false);
            inventorySlotArray[2].GetComponent<InventorySlot>().selectedSlot.SetActive(false);
            inventorySlotArray[3].GetComponent<InventorySlot>().selectedSlot.SetActive(false);
            inventorySlotArray[4].GetComponent<InventorySlot>().selectedSlot.SetActive(false);
            inventorySlotArray[5].GetComponent<InventorySlot>().selectedSlot.SetActive(false);
            if (inventorySlotArray[0].GetComponent<InventorySlot>().selectedSlot.activeSelf)
            {
                selectedSlotNum = 0;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            inventorySlotArray[1].GetComponent<InventorySlot>().selectedSlot.SetActive(!inventorySlotArray[1].GetComponent<InventorySlot>().selectedSlot.activeSelf);
            inventorySlotArray[0].GetComponent<InventorySlot>().selectedSlot.SetActive(false);
            inventorySlotArray[2].GetComponent<InventorySlot>().selectedSlot.SetActive(false);
            inventorySlotArray[3].GetComponent<InventorySlot>().selectedSlot.SetActive(false);
            inventorySlotArray[4].GetComponent<InventorySlot>().selectedSlot.SetActive(false);
            inventorySlotArray[5].GetComponent<InventorySlot>().selectedSlot.SetActive(false);
            if (inventorySlotArray[1].GetComponent<InventorySlot>().selectedSlot.activeSelf)
            {
                selectedSlotNum = 1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            inventorySlotArray[2].GetComponent<InventorySlot>().selectedSlot.SetActive(!inventorySlotArray[2].GetComponent<InventorySlot>().selectedSlot.activeSelf);
            inventorySlotArray[1].GetComponent<InventorySlot>().selectedSlot.SetActive(false);
            inventorySlotArray[0].GetComponent<InventorySlot>().selectedSlot.SetActive(false);
            inventorySlotArray[3].GetComponent<InventorySlot>().selectedSlot.SetActive(false);
            inventorySlotArray[4].GetComponent<InventorySlot>().selectedSlot.SetActive(false);
            inventorySlotArray[5].GetComponent<InventorySlot>().selectedSlot.SetActive(false);
            if (inventorySlotArray[2].GetComponent<InventorySlot>().selectedSlot.activeSelf)
            {
                selectedSlotNum = 2;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            inventorySlotArray[3].GetComponent<InventorySlot>().selectedSlot.SetActive(!inventorySlotArray[3].GetComponent<InventorySlot>().selectedSlot.activeSelf);
            inventorySlotArray[1].GetComponent<InventorySlot>().selectedSlot.SetActive(false);
            inventorySlotArray[2].GetComponent<InventorySlot>().selectedSlot.SetActive(false);
            inventorySlotArray[0].GetComponent<InventorySlot>().selectedSlot.SetActive(false);
            inventorySlotArray[4].GetComponent<InventorySlot>().selectedSlot.SetActive(false);
            inventorySlotArray[5].GetComponent<InventorySlot>().selectedSlot.SetActive(false);
            if (inventorySlotArray[3].GetComponent<InventorySlot>().selectedSlot.activeSelf)
            {
                selectedSlotNum = 3;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            inventorySlotArray[4].GetComponent<InventorySlot>().selectedSlot.SetActive(!inventorySlotArray[4].GetComponent<InventorySlot>().selectedSlot.activeSelf);
            inventorySlotArray[1].GetComponent<InventorySlot>().selectedSlot.SetActive(false);
            inventorySlotArray[2].GetComponent<InventorySlot>().selectedSlot.SetActive(false);
            inventorySlotArray[3].GetComponent<InventorySlot>().selectedSlot.SetActive(false);
            inventorySlotArray[0].GetComponent<InventorySlot>().selectedSlot.SetActive(false);
            inventorySlotArray[5].GetComponent<InventorySlot>().selectedSlot.SetActive(false);
            if (inventorySlotArray[4].GetComponent<InventorySlot>().selectedSlot.activeSelf)
            {
                selectedSlotNum = 4;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            inventorySlotArray[5].GetComponent<InventorySlot>().selectedSlot.SetActive(!inventorySlotArray[5].GetComponent<InventorySlot>().selectedSlot.activeSelf);
            inventorySlotArray[1].GetComponent<InventorySlot>().selectedSlot.SetActive(false);
            inventorySlotArray[2].GetComponent<InventorySlot>().selectedSlot.SetActive(false);
            inventorySlotArray[3].GetComponent<InventorySlot>().selectedSlot.SetActive(false);
            inventorySlotArray[4].GetComponent<InventorySlot>().selectedSlot.SetActive(false);
            inventorySlotArray[0].GetComponent<InventorySlot>().selectedSlot.SetActive(false);
            if (inventorySlotArray[5].GetComponent<InventorySlot>().selectedSlot.activeSelf)
            {
                selectedSlotNum = 5;
            }
        }
    }
    private void dropItem()
    {
        inventoryItemList[selectedSlotNum] = null;
        updateDisplayedInventory();
    }
    private void displayInventory()
    {
        showingInventory = true;
        int slotNumber = 6;
        int rows = Mathf.CeilToInt((float)numSlots / (float)numSlotsPerRow);
        for(int j = 0; j < rows; j++)
        {
            for(int i = 0; i < numSlotsPerRow; i++)
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
                else if(inventoryItemList[indexList].enu == ItemEnum.NULL)
                {
                    print("here");
                    if (ite.count > maxItem)
                    {
                        int overFlow = ite.count - maxItem;
                        ite.count = maxItem;
                        inventoryItemList[indexList] = ite;
                        Item overFlowItem = new Item(ite.itemName, ite.description, ite.icon, overFlow, ite.enu, ite.worldPrefab);
                        addItem(overFlowItem);
                    }
                    else
                    {
                        inventoryItemList[indexList] = ite;
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
            if(inventoryItemList[i] == null)
            {
                inventorySlotArray[i].GetComponent<InventorySlot>().emptySlot();
            }
        }
    }
}

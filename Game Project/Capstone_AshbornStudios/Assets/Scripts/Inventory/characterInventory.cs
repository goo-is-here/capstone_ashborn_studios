using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class characterInventory : MonoBehaviour
{
    [SerializeField] Item[] inventoryItemList;
    List<GameObject> inventorySlotArray;
    [SerializeField] int hotBarSlots = 6;
    [SerializeField] int numSlots = 12;
    [SerializeField] int numSlotsPerRow = 6;
    [SerializeField] float spawnDistance = 5f;
    [SerializeField] float throwSpeed = 2f;
    public GameObject slotPrefab;
    public GameObject hotBarSlotParent;
    public GameObject[] inventorySlotParent; 
    PlayerController player;
    bool showingInventory = false;
    [SerializeField] int maxItem = 99;
    public int selectedSlotNum = -1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventoryItemList = new Item[hotBarSlots + numSlots];
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        //sets intial amount for array
        inventorySlotArray = new List<GameObject>();
        //creates hotbar slots
        for(int i = 0; i < hotBarSlots; i++)
        {
            GameObject slot = Instantiate(slotPrefab, hotBarSlotParent.transform);
            inventorySlotArray.Add(slot);
        }
        updateSlotNumber();
        updateDisplayedInventory();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I)){
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
        if(selectedSlotNum >= 0 || selectedSlotNum < inventoryItemList.Length)
        {
            print(player.gameObject);
            Vector3 pos = player.transform.TransformPoint(Vector3.forward * spawnDistance);
            GameObject drop = Instantiate(inventoryItemList[selectedSlotNum].worldPrefab, pos, player.transform.rotation);
            drop.GetComponent<Rigidbody>().AddForce(player.transform.forward * throwSpeed);
            pickUpItem dropVariables = drop.GetComponent<pickUpItem>();
            dropVariables.count = inventoryItemList[selectedSlotNum].count;
            inventoryItemList[selectedSlotNum] = null;
            updateDisplayedInventory();
        }
    }
    private void displayInventory()
    {
        player.canMove = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
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
        updateSlotNumber();
        updateDisplayedInventory();
    }
    private void hideInventory()
    {
        player.canMove = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        showingInventory = false;
        for (int i = hotBarSlots; i < inventorySlotArray.Count; i++)
        {
            Destroy(inventorySlotArray[i].gameObject);
        }
        inventorySlotArray.RemoveRange(hotBarSlots, numSlots-hotBarSlots);
    }
    //to be rewritten and condensed
    public void addItem(Item ite, pickUpItem obj = null)
    {
        if (countInventory() == 0)
        {
            if (ite.count > maxItem)
            {
                int overFlow = ite.count - maxItem;
                ite.count = maxItem;
                inventoryItemList[0] = ite;
                Item itemTemp = new Item(ite.itemName, ite.description, ite.icon, overFlow, ite.enu, ite.worldPrefab);
                addItem(itemTemp, obj);
            }
            else
            {
                inventoryItemList[0] = ite;
                if(obj != null)
                {
                    obj.itemDestroy();
                }
            }
            
        }
        else
        {
            bool added = false;
            int indexList = 0;
            while(!added && indexList < inventoryItemList.Length)
            {
                if (inventoryItemList[indexList] == null || inventoryItemList[indexList].enu == ItemEnum.NULL)
                {
                    if (ite.count > maxItem)
                    {
                        int overFlow = ite.count - maxItem;
                        ite.count = maxItem;
                        inventoryItemList[indexList] = ite;
                        Item itemTemp = new Item(ite.itemName, ite.description, ite.icon, overFlow, ite.enu, ite.worldPrefab);
                        addItem(itemTemp, obj);
                    }
                    else
                    {
                        inventoryItemList[indexList] = ite;
                        if (obj != null)
                        {
                            obj.itemDestroy();
                        }
                    }
                    added = true;
                    
                }
                else if (inventoryItemList[indexList].enu == ite.enu && inventoryItemList[indexList].count < maxItem)
                {
                    int newCount = inventoryItemList[indexList].count + ite.count;
                    if (newCount <= maxItem)
                    {
                        inventoryItemList[indexList].count = newCount;
                        if (obj != null)
                        {
                            obj.itemDestroy();
                        }
                    }
                    else
                    {
                        int overFlow = newCount - maxItem;
                        inventoryItemList[indexList].count = maxItem;
                        Item itemTemp = new Item(ite.itemName, ite.description, ite.icon, overFlow, ite.enu, ite.worldPrefab);
                        addItem(itemTemp, obj);
                    }
                    added = true;
                    
                }
                
                indexList++;
            }
        }
        updateDisplayedInventory();
    }
    public void removeItem(Item ite, int amount)
    {
        for(int i = 0; i < inventoryItemList.Length; i++)
        {
            if (inventoryItemList[i].enu == ite.enu && amount >= 0)
            {
                if (amount >= inventoryItemList[i].count)
                {
                    amount -= inventoryItemList[i].count;
                    inventoryItemList[i] = null;
                }
                else
                {
                    inventoryItemList[i].count -= amount;
                    amount = -1;
                }
            }
        }
        updateDisplayedInventory();
    }
    private int countInventory()
    {
        int capacity = 0;
        for(int i = 0; i < inventoryItemList.Length; i++)
        {
            if(inventoryItemList[i] != null && inventoryItemList[i].enu != ItemEnum.NULL)
            {
                capacity++;
            }
        }
        return capacity;
    }
    private void updateDisplayedInventory()
    {
        for(int i = 0; i < inventorySlotArray.Count; i++)
        {
            
            if(inventoryItemList[i] == null || inventoryItemList[i].enu == ItemEnum.NULL)
            {
                inventorySlotArray[i].GetComponent<InventorySlot>().emptySlot();
            }
            else
            {
                inventorySlotArray[i].GetComponent<InventorySlot>().setSlot(inventoryItemList[i]);
            }
        }
    }
    private void updateSlotNumber()
    {
        for(int i = 0; i < inventorySlotArray.Count; i++)
        {
            inventorySlotArray[i].GetComponent<InventorySlot>().slotIndex = i;
        }
    }
    public void swapItems(int targetSlot)
    {
        Item tempItem = inventoryItemList[targetSlot];
        inventoryItemList[targetSlot] = inventoryItemList[selectedSlotNum];
        inventoryItemList[selectedSlotNum] = tempItem;
        inventorySlotArray[selectedSlotNum].GetComponent<InventorySlot>().selectedSlot.SetActive(false);
        selectedSlotNum = -1;
        updateDisplayedInventory();
    }
}

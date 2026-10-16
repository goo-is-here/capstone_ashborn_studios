using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class pickUpBag : MonoBehaviour, IDataPersistence
{
    //this is the list of all items held in the bag object
    public List<Item> inventoryHold;
    //if the bag is present on the level
    public bool active;
    //the player inventory
    public characterInventory invent;
    //position of the bag
    Vector3 pos;
    
    void Start()
    {
        //sets it based on loaded data and the player inventory
        gameObject.SetActive(active);
        //invent = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        transform.position = pos;
    }
    //loading the position, invetory, and active bool
    public void LoadData(GameData data)
    {
        pos = data.droppedPosition;
        inventoryHold.Clear();
        active = data.droppedInventory;
        foreach(Item ite in data.droppedContents)
        {
            
            inventoryHold.Add(ite);
        }
    }
    //saves whether it's active, position, and inventory
    public void SaveData(ref GameData data)
    {
        data.droppedInventory = active;
        data.droppedContents.Clear();
        data.droppedPosition = pos;
        foreach(Item ite in inventoryHold)
        {
            data.droppedContents.Add(ite);
        }
    }
    //adds item to the bag
    public void addItems()
    {

        //for each item in the inventory
        for (int i = 0; i < invent.inventoryItemList.Length; i++)
        {
            //if not null
            if (invent.inventoryItemList[i] != null)
            {
                //add the item to this list and remove it from the player inventory
                Item itemToAdd = new Item(invent.inventoryItemList[i].itemName, invent.inventoryItemList[i].description, invent.inventoryItemList[i].icon, invent.inventoryItemList[i].count, invent.inventoryItemList[i].enu, invent.inventoryItemList[i].worldPrefab);
                inventoryHold.Add(itemToAdd);
                invent.removeItem(invent.inventoryItemList[i].enu, invent.inventoryItemList[i].count);
            }
        }

        //activate it and record the position
        active = true;
        pos = transform.position;
    }
    //picks up the bag
    private void OnTriggerEnter(Collider other)
    {
        //if colliding with player
        if (other.CompareTag("Player"))
        {
            //sets it to false
            active = false;
            //adds each item to the player inventory
            foreach (Item ite in inventoryHold)
            {
                invent.addItem(ite);
            }
        }
        //clears the bag inventory and set to false
        inventoryHold.Clear();
        this.gameObject.SetActive(false);
    }
}

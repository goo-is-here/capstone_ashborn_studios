using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class pickUpBag : MonoBehaviour, IDataPersistence
{
    public List<Item> inventoryHold;
    public bool active;
    public Inventory invent;
    Vector3 pos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.SetActive(active);
        invent = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        transform.position = pos;
    }
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
    public void addItems()
    {
        for(int i = 0; i < invent.Items.Count; i++)
        {
            Item ite = invent.GetItemAtIndex(i);
            if(ite != null)
            {
                Item itemToAdd = new Item(ite.name, ite.description, ite.icon, ite.count, ite.enu, ite.worldPrefab);
                inventoryHold.Add(itemToAdd);
                invent.RemoveItemAtIndex(i, ite.count);
            }
        }
        active = true;
        pos = transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        active = false;
        if (other.CompareTag("Player"))
        {
            foreach(Item ite in inventoryHold)
            {
                invent.AddItem(ite);
            }
        }
        inventoryHold.Clear();
        this.gameObject.SetActive(false);
    }
}

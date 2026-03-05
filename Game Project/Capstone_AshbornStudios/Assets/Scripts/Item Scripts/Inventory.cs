using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    public List<Item> Items = new List<Item>();

    void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public void AddItem(Item itemToAdd)
    {
        bool itemExists = false;

        foreach (Item item in Items)
        {
            if (item.name == itemToAdd.name)
            {
                int newCount = item.count + itemToAdd.count;

                if (newCount <= 99)
                {
                    item.count = newCount;
                }
                else
                {
                    item.count = 99;
                    int overflow = newCount - 99;

                    Items.Add(new Item(itemToAdd.name, overflow));
                }

                itemExists = true;
                break;
            }
        }

        if (!itemExists)
        {
            Items.Add(itemToAdd);
        }

        Debug.Log(itemToAdd.count + " " + itemToAdd.name + " added to inventory.");
    }
}
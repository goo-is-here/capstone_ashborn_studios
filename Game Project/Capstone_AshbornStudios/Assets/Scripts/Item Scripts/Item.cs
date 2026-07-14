using UnityEngine;
using System.Collections;

[System.Serializable]
public class Item 
{
    public string itemName;
    public string description;
    public Sprite icon;
    public int count;
    public ItemEnum enu;
    public GameObject worldPrefab;
    bool collected = false;

    public Item(string name, string description, Sprite icon, int count, ItemEnum enu, GameObject worldPrefab)
    {
        this.itemName = name;
        this.description = description;
        this.icon = icon;
        this.count = count;
        this.enu = enu;
        this.worldPrefab = worldPrefab;
    }
    public void use()
    {
        return;
    }

}
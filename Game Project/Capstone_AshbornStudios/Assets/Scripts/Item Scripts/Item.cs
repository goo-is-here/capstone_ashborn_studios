using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string name;
    public string description;
    public Sprite icon;
    public int count;
    public GameObject worldPrefab;


    public Item(string name, string description, Sprite icon,  int count, GameObject worldPrefab = null )
    {
        this.name = name;
        this.description = description;
        this.icon = icon;
        this.count = count;
        this.worldPrefab = worldPrefab;
}
}
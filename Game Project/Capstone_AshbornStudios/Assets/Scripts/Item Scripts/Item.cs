using UnityEngine;

[System.Serializable]
public class Item
{
    public string name;
    public string description;
    public Sprite icon;
    public int count;
    public ItemEnum enu;
    public GameObject worldPrefab;

    public Item(string name, string description, Sprite icon, int count, ItemEnum enu, GameObject worldPrefab)
    {
        this.name = name;
        this.description = description;
        this.icon = icon;
        this.count = count;
        this.enu = enu;
        this.worldPrefab = worldPrefab;
    }
}
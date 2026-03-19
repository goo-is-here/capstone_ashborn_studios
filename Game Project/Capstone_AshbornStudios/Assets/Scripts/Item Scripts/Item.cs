using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string name;
    public Sprite icon;
    public int count;

    public Item(string name, Sprite icon,  int count)
    {
        this.name = name;
        this.icon = icon;
        this.count = count;
    }
}
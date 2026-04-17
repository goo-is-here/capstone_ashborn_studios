using UnityEngine;

[System.Serializable]
public class Treasure_Item
{
    public string treasureID;
    public string treasureName;
    public string description;
    public Sprite icon;

    public Treasure_Item(string id, string name, string desc, Sprite treasureIcon)
    {
        treasureID = id;
        treasureName = name;
        description = desc;
        icon = treasureIcon;
    }
}


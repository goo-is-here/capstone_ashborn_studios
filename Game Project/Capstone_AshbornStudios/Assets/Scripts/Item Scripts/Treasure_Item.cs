using UnityEngine;

[System.Serializable]
public class Treasure_Item
{
    public string treasureID;
    public string treasureName;
    public string description;
    

    public Treasure_Item(string id, string name, string desc)
    {
        treasureID = id;
        treasureName = name;
        description = desc;
       
    }
}


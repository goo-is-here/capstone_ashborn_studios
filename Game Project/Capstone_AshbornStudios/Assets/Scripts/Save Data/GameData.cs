using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public float damageVal;
    public float durability;
    public float maxDurability;
    public int scenceIndex;
    public Vector3 hubPosition;
    public Vector3 minePosition;
    public List<Item> inventory;

    public GameData()
    {
        inventory = new List<Item>();
        minePosition = new Vector3(0, 0, 0);
        hubPosition = new Vector3(0, 0, 0);
        this.scenceIndex = 1;
        this.damageVal = 10;
        this.maxDurability = 100;
        this.durability = maxDurability;
    }
}

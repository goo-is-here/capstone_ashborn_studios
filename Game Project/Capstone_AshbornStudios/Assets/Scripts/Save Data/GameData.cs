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
    public List<Vector3> minedBlocks;
    public List<string> treasureHolder;
    public GameData()
    {
        treasureHolder = new List<string>();
        this.inventory = new List<Item>();
        this.minedBlocks = new List<Vector3>();
        this.minePosition = new Vector3(0, 0, 0);
        this.hubPosition = new Vector3(4, 16, -13);
        this.scenceIndex = 1;
        this.damageVal = 10;
        this.maxDurability = 100;
        this.durability = maxDurability;
    }
}

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
    public GameData()
    {
        inventory = new List<Item>();
        minedBlocks = new List<Vector3>();
        minePosition = new Vector3(2, 1, 9);
        hubPosition = new Vector3(4, 16, -13);
        this.scenceIndex = 1;
        this.damageVal = 10;
        this.maxDurability = 100;
        this.durability = maxDurability;
    }
}

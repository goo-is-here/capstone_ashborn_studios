using System.Collections.Generic;
using System;
using UnityEngine;
using System.Collections;

public class Treasure_Manager : MonoBehaviour
{
    public static Treasure_Manager Instance;

    [Header("Treasure Storage")]
    public List<Treasure_Item> collectedTreasures = new List<Treasure_Item>();

    [Header("Treasure State Variable")]
    public List<string> collectedTreasureIDs = new List<string>();

    [Header("Collectable Treasure Items")]
    public Treasure_Pickup[] collectableTreasures;

    [Header("Pedestal Treasure Items")]
    public Pedestal_Treasure_Item[] pedestalItems;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddTreasure(Treasure_Item newTreasure)
    {
        if (!collectedTreasureIDs.Contains(newTreasure.treasureID))
        {
            collectedTreasureIDs.Add(newTreasure.treasureID);
            collectedTreasures.Add(newTreasure);

            Debug.Log("Collected treasure: " + newTreasure.treasureName);
            Debug.Log("Collected IDs count now: " + collectedTreasureIDs.Count);

            RefreshPedestalTreasures();
        }
        else
        {
            Debug.Log("Treasure already collected: " + newTreasure.treasureName);
        }
    }
    public bool HasTreasure(string treasureID)
    {
        return collectedTreasureIDs.Contains(treasureID);
    }

    public int GetTotalTreasureCount()
    {
        HashSet<string> uniqueIDs = new HashSet<string>();

        if (collectableTreasures != null)
        {
            foreach (Treasure_Pickup treasure in collectableTreasures)
            {
                if (treasure == null) continue;
                if (!string.IsNullOrEmpty(treasure.treasureID))
                    uniqueIDs.Add(treasure.treasureID);
            }
        }

        if (pedestalItems != null)
        {
            foreach (Pedestal_Treasure_Item item in pedestalItems)
            {
                if (item == null) continue;
                if (!string.IsNullOrEmpty(item.treasureID))
                    uniqueIDs.Add(item.treasureID);
            }
        }

        return uniqueIDs.Count;
    }

    public bool HasAllTreasures()
    {
        int totalCollectables = collectableTreasures != null ? collectableTreasures.Length : 0;
        int collectedCount = collectedTreasureIDs.Count;
        print(collectedCount);
        Debug.Log("CollectedTreasureIDs Count = " + collectedCount);
        Debug.Log("CollectableTreasures Length = " + totalCollectables);

        return totalCollectables > 0 && collectedCount >= totalCollectables;
    }

    public void RefreshPedestalTreasures()
    {
        if (pedestalItems == null) return;

        foreach (Pedestal_Treasure_Item item in pedestalItems)
        {
            if (item == null) continue;
            item.RefreshState();
        }
    }
    IEnumerator ApplyTreasureState()
    {
        yield return null;

        collectableTreasures = FindObjectsOfType<Treasure_Pickup>();
        pedestalItems = FindObjectsOfType<Pedestal_Treasure_Item>();

        RefreshPedestalTreasures();

        Debug.Log("Treasure state applied. Count: " + collectedTreasureIDs.Count);
    }
}



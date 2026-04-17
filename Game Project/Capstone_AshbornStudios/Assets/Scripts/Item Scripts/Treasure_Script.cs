using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Treasure_Script : MonoBehaviour
{
    public string treasureID = "Treasure ID";
    public string treasureName = "Treasure Name";
    public string description = "Treasure Description.";
    public Sprite icon;

    [Header("What to destroy after pickup")]
    public GameObject objectToDestroy;

    public void Pickup()
    {
        Treasure_Item treasure = new Treasure_Item(
            treasureID,
            treasureName,
            description,
            icon
        );

        if (Treasure_Manager.Instance != null)
        {
            Treasure_Manager.Instance.AddTreasure(treasure);
        }
        else
        {
            Debug.LogWarning("TreasureManager instance not found.");
        }

        if (objectToDestroy != null)
            Destroy(objectToDestroy);
        else
            Destroy(gameObject);
    }
}


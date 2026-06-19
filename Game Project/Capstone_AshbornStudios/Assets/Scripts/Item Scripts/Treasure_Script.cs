using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Treasure_Script : MonoBehaviour
{
    //TODO replace all of this, this is bad
    //treasure details
    public string treasureID = "Treasure ID";
    public string treasureName = "Treasure Name";
    public string description = "Treasure Description.";
    

    [Header("What to destroy after pickup")]
    public GameObject objectToDestroy;

    public void Pickup()
    {
        //makes a new item
        Treasure_Item treasure = new Treasure_Item(
            treasureID,
            treasureName,
            description
          
        );
        //checks manager is not null
        if (Treasure_Manager.Instance != null)
        {
            Treasure_Manager.Instance.AddTreasure(treasure);
        }
        else
        {
            Debug.LogWarning("TreasureManager instance not found.");
        }
        //detroys object
        //TODO BAD replace
        if (objectToDestroy != null)
            Destroy(objectToDestroy);
        else
            Destroy(gameObject);
    }
}


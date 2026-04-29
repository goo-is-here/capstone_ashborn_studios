using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PedestalTreasureEntry
{
    public string treasureID;
    public GameObject treasureObject;
}

public class Treasure_Pedestal : MonoBehaviour
{
    [Header("Treasures That Belong On This Pedestal")]
    public List<PedestalTreasureEntry> treasuresOnPedestal = new List<PedestalTreasureEntry>();

    private void Start()
    {
        RefreshPedestal();
    }

    public void RefreshPedestal()
    {
        if (Treasure_Manager.Instance == null)
        {
            Debug.LogWarning("Treasure_Manager instance not found.");
            return;
        }

        for (int i = 0; i < treasuresOnPedestal.Count; i++)
        {
            if (treasuresOnPedestal[i] == null || treasuresOnPedestal[i].treasureObject == null)
                continue;

            bool collected = Treasure_Manager.Instance.HasTreasure(treasuresOnPedestal[i].treasureID);
            treasuresOnPedestal[i].treasureObject.SetActive(collected);

            //Debug.Log("Pedestal " + gameObject.name + " treasure " + treasuresOnPedestal[i].treasureID + " set to " + collected);
        }
    }
}
            
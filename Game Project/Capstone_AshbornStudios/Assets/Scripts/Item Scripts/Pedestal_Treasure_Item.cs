using UnityEngine;

public class Pedestal_Treasure_Item : MonoBehaviour
{
    public string treasureID;

    private void Start()
    {
        RefreshState();
    }

    public void RefreshState()
    {
        if (Treasure_Manager.Instance == null)
        {
            Debug.LogWarning("Treasure_Manager not found.");
            gameObject.SetActive(false);
            return;
        }

        bool collected = Treasure_Manager.Instance.HasTreasure(treasureID);
        gameObject.SetActive(collected);

        Debug.Log("Treasure item " + treasureID + " set to " + collected);
    }
}

using Unity.VisualScripting;
using UnityEngine;

public class Treasure_Pickup : MonoBehaviour
{
    public string treasureID = "ancient_relic";
    public string treasureName = "Ancient Relic";
    public string treasureDescription = "A valuable relic recovered from deep underground.";
    public Sprite treasureIcon;

    [Header("What to destroy after pickup")]
    public GameObject objectToDestroy;

    private bool pickedUp = false;

    private void Awake()
    {
        if (objectToDestroy == null)
            objectToDestroy = gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Treasure trigger hit by: " + other.name);

        if (pickedUp) return;
        if (!other.CompareTag("Player"))
        {
            Debug.Log("Not tagged Player");
            return;
        }

        if (Treasure_Manager.Instance == null)
        {
            Debug.LogWarning("Treasure manager instance not found!");
            return;
        }

        pickedUp = true;

        Treasure_Item newTreasure = new Treasure_Item(
            treasureID,
            treasureName,
            treasureDescription,
            treasureIcon
        );

        Treasure_Manager.Instance.AddTreasure(newTreasure);

        Debug.Log("Collected treasure: " + treasureName);

        Destroy(objectToDestroy);
    }
}

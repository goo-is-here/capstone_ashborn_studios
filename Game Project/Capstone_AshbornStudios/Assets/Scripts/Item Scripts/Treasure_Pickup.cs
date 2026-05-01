using Unity.VisualScripting;
using UnityEngine;

public class Treasure_Pickup : MonoBehaviour
{
    public string treasureID = "ancient_relic";
    public string treasureName = "Ancient Relic";
    public string treasureDescription = "A valuable relic recovered from deep underground.";
    GameObject player;

    [Header("What to destroy after pickup")]
    public GameObject objectToDestroy;

    private bool pickedUp = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (objectToDestroy == null)
            objectToDestroy = gameObject;
    }

    private void Start()
    {
        CheckIfAlreadyCollected();
    }
    private void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position) < 2f)
        {
            if (pickedUp) return;

            if (Treasure_Manager.Instance == null)
            {
                Debug.LogWarning("Treasure_Manager instance not found!");
                return;
            }

            if (Treasure_Manager.Instance.HasTreasure(treasureID))
            {
                Destroy(objectToDestroy);
                return;
            }

            pickedUp = true;

            Treasure_Item newTreasure = new Treasure_Item(
                treasureID,
                treasureName,
                treasureDescription
            );

            Treasure_Manager.Instance.AddTreasure(newTreasure);

            Debug.Log("Collected treasure: " + treasureID);

            Destroy(objectToDestroy);
        }
    }
    private void CheckIfAlreadyCollected()
    {
        if (Treasure_Manager.Instance == null) return;

        if (Treasure_Manager.Instance.HasTreasure(treasureID))
        {
            Debug.Log("Treasure already collected, removing: " + treasureID);
            Destroy(objectToDestroy);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (pickedUp) return;
        if (!other.CompareTag("Player")) return;

        if (Treasure_Manager.Instance == null)
        {
            Debug.LogWarning("Treasure_Manager instance not found!");
            return;
        }

        if (Treasure_Manager.Instance.HasTreasure(treasureID))
        {
            Destroy(objectToDestroy);
            return;
        }

        pickedUp = true;

        Treasure_Item newTreasure = new Treasure_Item(
            treasureID,
            treasureName,
            treasureDescription
        );

        Treasure_Manager.Instance.AddTreasure(newTreasure);

        Debug.Log("Collected treasure: " + treasureID);

        Destroy(objectToDestroy);
    }
}

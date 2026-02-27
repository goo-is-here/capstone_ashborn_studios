using Unity.VisualScripting;
using UnityEngine;

public class Item_Pickup : MonoBehaviour
{
    [Header("Assign in Inspector")]
    public GameObject item;
    public int amount = 1;

    private void Reset()
    {
        // Make sure this collider is a trigger
        Collider col = GetComponent<Collider>();
        if (col != null) col.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"TRIGGER ENTER: {name} touched by {other.name}");

        if (!other.CompareTag("Player"))
        {
            Debug.Log("Not player, ignoring.");
            return;
        }

        if (item == null)
        {
            Debug.LogError($"Pickup '{name}' has NO BaseItem assigned!", this);
            return;
        }

        
        Destroy(gameObject);
    }


}
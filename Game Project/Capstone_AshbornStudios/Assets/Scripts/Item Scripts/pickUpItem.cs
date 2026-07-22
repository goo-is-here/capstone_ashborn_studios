using UnityEngine;

public class pickUpItem : MonoBehaviour
{
    public string itemName;
    public string description;
    public Sprite icon;
    public int count;
    public ItemEnum enu;
    public GameObject worldPrefab;
    bool collected = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !collected)
        {
            collected = true;
            Item addThis = new Item(itemName, description, icon, count, enu, worldPrefab);
            other.gameObject.GetComponent<PlayerController>().addItemInventory(gameObject);
        }
    }
}

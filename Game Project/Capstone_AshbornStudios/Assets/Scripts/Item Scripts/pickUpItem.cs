using UnityEngine;
using System.Collections;
public class pickUpItem : MonoBehaviour
{
    public string itemName;
    public string description;
    public Sprite icon;
    public int count = 1;
    public ItemEnum enu;
    public BlockObject worldPrefab;
    bool collected = false;
    //gets player
    GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.tag);
        if (other.gameObject.CompareTag("Player") && !collected)
        {
            collected = true;
            Item ite = new Item(itemName, description, icon, count, enu, worldPrefab.dropped);
            player.gameObject.GetComponent<PlayerController>().addItemInventory(ite, this);
            itemDestroy();
        }
    }
    public void itemDestroy()
    {
        Destroy(this.gameObject);
    }
}

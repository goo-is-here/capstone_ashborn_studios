using UnityEngine;
using System.Collections.Generic;

public class craftingBench : MonoBehaviour
{
    public GameObject player;
    public Inventory invent;
    public List<Item> allItems = new List<Item>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        invent = player.GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position) < 5f && Input.GetKeyDown(KeyCode.E))
        {
            allItems.Clear();
            List<Item> inventory = invent.Items;
            for(int i = 0; i < inventory.Count; i++)
            {
                bool found = false;
                for(int j = 0; j < allItems.Count; j++)
                {
                    if(inventory[i] != null && allItems[j] != null && allItems[j].enu == inventory[i].enu)
                    {
                        allItems[j].count += inventory[i].count;
                        found = true;
                    }
                }
                if (!found && inventory[i] != null)
                {
                    allItems.Add(inventory[i]);
                }
            }
        }
    }
}

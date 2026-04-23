using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_script : MonoBehaviour
{
    public Inventory inventory;
    public TMP_Text[] slotCounts;

    private void Start()
    {
        if (inventory == null)
            inventory = Inventory.Instance;
    }

    private void Update()
    {
        RefreshUI();
    }

    public void RefreshUI()
    {
        if (inventory == null) return;

        for (int i = 0; i < slotCounts.Length; i++)
        {
            if (i < inventory.Items.Count && inventory.Items[i] != null)
            {
                slotCounts[i].text = inventory.Items[i].count.ToString();
            }
            else
            {
                slotCounts[i].text = "0";
            }
        }
    }

    public void UpdateInventoryUI(List<Item> items)
    {
      
       
    }
}
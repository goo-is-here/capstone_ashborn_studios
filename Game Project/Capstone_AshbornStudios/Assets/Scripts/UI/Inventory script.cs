using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro; 

public class InventoryUI : MonoBehaviour 
{
    [System.Serializable]
    public class UISlot
    {
        public Image icon;
        public TMP_Text amountText;
    }

    public List<UISlot> slots = new List<UISlot>();

    public void UpdateInventoryUI(List<Item> items)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (i < items.Count && items[i] != null)
            {
                slots[i].icon.sprite = items[i].icon;
                slots[i].icon.enabled = true;

                slots[i].amountText.text = items[i].count.ToString();
                slots[i].amountText.enabled = true;
            }
            else
            {
                slots[i].icon.sprite = null;
                slots[i].icon.enabled = false;

                slots[i].amountText.text = "";
                slots[i].amountText.enabled = false;
            }
        }
    }
}

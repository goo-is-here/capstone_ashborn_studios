using System.Collections.Generic;
using UnityEngine;
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
                if (slots[i].icon != null)
                {
                    slots[i].icon.sprite = items[i].icon;
                    slots[i].icon.enabled = true;
                }

                if (slots[i].amountText != null)
                {
                    slots[i].amountText.text = items[i].count.ToString();
                    slots[i].amountText.enabled = true;
                }
            }
            else
            {
                if (slots[i].icon != null)
                {
                    slots[i].icon.sprite = null;
                    slots[i].icon.enabled = false;
                }

                if (slots[i].amountText != null)
                {
                    slots[i].amountText.text = "";
                    slots[i].amountText.enabled = false;
                }
            }
        }
    }
}
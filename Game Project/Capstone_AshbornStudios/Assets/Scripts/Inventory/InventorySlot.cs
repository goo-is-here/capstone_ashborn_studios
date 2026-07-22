using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public TextMeshProUGUI countNumber;
    public Image itemSprite;
    public Item itemStored;
    public GameObject selectedSlot;
    public int slotIndex = -1;
    characterInventory inventory;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<characterInventory>();
    }
    public void setSlot(Item ite)
    {
        if (ite == null) return;
        itemStored = ite;
        itemSprite.GetComponent<Image>().color = new Color(itemSprite.GetComponent<Image>().color.r, itemSprite.GetComponent<Image>().color.g, itemSprite.GetComponent<Image>().color.b, 1);
        itemSprite.sprite = itemStored.icon;
        countNumber.text = "" + itemStored.count;
    }
    public void setSprite()
    {
        itemSprite.GetComponent<Image>().color = new Color(itemSprite.GetComponent<Image>().color.r, itemSprite.GetComponent<Image>().color.g, itemSprite.GetComponent<Image>().color.b, 1);
        itemSprite.sprite = itemStored.icon;
    }
    public void setCount()
    {
        countNumber.text = "" + itemStored.count;
    }
    public void emptySlot()
    {
        itemSprite.GetComponent<Image>().color = new Color(itemSprite.GetComponent<Image>().color.r, itemSprite.GetComponent<Image>().color.g, itemSprite.GetComponent<Image>().color.b, 0);
        itemSprite.sprite = null;
        countNumber.text = "0";
    }
    public void selectSlot()
    {
        if(inventory != null)
        {
            if(inventory.selectedSlotNum < 0)
            {
                inventory.selectedSlotNum = slotIndex;
                selectedSlot.SetActive(true);
            }
            else
            {
                inventory.swapItems(slotIndex);
                selectedSlot.SetActive(false);
            }

        }
    }
}

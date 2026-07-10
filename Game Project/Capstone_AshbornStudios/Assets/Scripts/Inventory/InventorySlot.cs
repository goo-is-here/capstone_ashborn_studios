using UnityEngine;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public TextMeshProUGUI countNumber;
    public Sprite itemSprite;
    public Item itemStored;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        emptySlot();
    }
    public void setSlot(Item ite)
    {
        if (ite == null) return;
        itemStored = ite;
        itemSprite = itemStored.icon;
        countNumber.text = "" + itemStored.count;
    }
    public void setSprite()
    {
        itemSprite = itemStored.icon;
    }
    public void setCount()
    {
        countNumber.text = "" + itemStored.count;
    }
    public void emptySlot()
    {
        itemSprite = null;
        countNumber.text = "0";
    }
}

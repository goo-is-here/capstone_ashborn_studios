using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public TextMeshProUGUI countNumber;
    public Image itemSprite;
    public Item itemStored;
    public GameObject selectedSlot;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        emptySlot();
    }
    public void setSlot(Item ite)
    {
        if (ite == null) return;
        itemStored = ite;
        itemSprite.sprite = itemStored.icon;
        countNumber.text = "" + itemStored.count;
    }
    public void setSprite()
    {
        itemSprite.sprite = itemStored.icon;
    }
    public void setCount()
    {
        countNumber.text = "" + itemStored.count;
    }
    public void emptySlot()
    {
        itemSprite.sprite = null;
        countNumber.text = "0";
    }
}

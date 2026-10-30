using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MineralPopup : MonoBehaviour
{
    public TMP_Text itemText;
    public Image icon;
    public float duration = 5.0f;

    public void SpawnPopup(string itemName, Sprite newIcon, int numCollected)
    {
        itemText.text = $"+{numCollected} {itemName}";
        if(icon != null)
        {
            icon.sprite = newIcon;
        }

        Destroy(gameObject, duration);
    }
}

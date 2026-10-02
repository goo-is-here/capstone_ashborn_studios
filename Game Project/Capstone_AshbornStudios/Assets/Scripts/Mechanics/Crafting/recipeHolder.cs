using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class recipeHolder : MonoBehaviour
{
    [SerializeField] GameObject icon;
    [SerializeField] GameObject text;

    public void setText(string textInsert)
    {
        text.GetComponent<TextMeshProUGUI>().text = textInsert;
    }
    public void setImage(Sprite spriteInset)
    {
        icon.GetComponent<Image>().sprite = spriteInset;
    }
}

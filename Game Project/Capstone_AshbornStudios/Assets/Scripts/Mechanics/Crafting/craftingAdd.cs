using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class craftingAdd : MonoBehaviour
{
    public int recipeToMake;
    craftingBench bench;
    TextMeshProUGUI itemName;
    TextMeshProUGUI itemDescription;
    GameObject craft;
    public string craftName;
    public string description;
    public bool make;
    private void Start()
    {
        bench = GameObject.FindGameObjectWithTag("Crafting").GetComponent<craftingBench>();
        itemName = GameObject.Find("ItemName").GetComponent<TextMeshProUGUI>();
        itemDescription = GameObject.Find("ItemDescription").GetComponent<TextMeshProUGUI>();
        craft = GameObject.Find("CraftButton");
    }
    public void add()
    {
        bench.Make(recipeToMake);
        craft.GetComponent<Button>().interactable = bench.checkMade(recipeToMake);
    }
    public void select()
    {
        itemName.text = craftName;
        itemDescription.text = description;
        craft.GetComponent<Button>().interactable = make;
        craft.GetComponent<craftingAdd>().recipeToMake = recipeToMake;
    }
}

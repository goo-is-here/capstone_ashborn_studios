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
    public int repairAmt;
    public int damageUpgrade;
    PlayerController controller;
    public bool upgrade;
    public bool repair;
    private void Start()
    {
        bench = GameObject.FindGameObjectWithTag("Crafting").GetComponent<craftingBench>();
        itemName = GameObject.Find("ItemName").GetComponent<TextMeshProUGUI>();
        itemDescription = GameObject.Find("ItemDescription").GetComponent<TextMeshProUGUI>();
        craft = GameObject.Find("CraftButton");
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    public void add()
    {
        
        if (repair)
        {
            controller.durability = controller.maxDurability;
            craft.GetComponent<Button>().interactable = false;
            bench.upgradeRepair(recipeToMake);
        }
        else if(upgrade)
        {
            controller.durability = controller.maxDurability;
            controller.damageVal = damageUpgrade;
            craft.GetComponent<Button>().interactable = false;
            bench.upgradeRepair(recipeToMake);
        }
        else
        {
            bench.Make(recipeToMake);
            craft.GetComponent<Button>().interactable = bench.checkMade(recipeToMake);

        }
    }
    public void select()
    {
        itemName.text = craftName;
        itemDescription.text = description;
        craft.GetComponent<craftingAdd>().damageUpgrade = damageUpgrade;
        craft.GetComponent<Button>().interactable = make;
        craft.GetComponent<craftingAdd>().recipeToMake = recipeToMake;
        craft.GetComponent<craftingAdd>().upgrade = upgrade;
        craft.GetComponent<craftingAdd>().repair = repair;

    }
}

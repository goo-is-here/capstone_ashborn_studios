using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class craftingBench : MonoBehaviour
{
    public GameObject player;
    public Inventory invent;
    public List<Item> allItems = new List<Item>();
    public recipeNode[] recipes;
    public GameObject uiSlot;
    public Transform container;
    public GameObject craftingUi;
    public GameObject text;
    public recipeNode[] tools;
    GameObject[] slots;
    GameObject[] toolSlots;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        invent = player.GetComponent<Inventory>();
        slots = new GameObject[recipes.Length];
        toolSlots = new GameObject[tools.Length];
        craftingUi.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position) < 5f && Input.GetKeyDown(KeyCode.E))
        {
            //craftingUi.SetActive(!craftingUi.activeSelf);
            if (!craftingUi.activeSelf)
            {
                craftingUi.SetActive(true);
                allItems.Clear();
                List<Item> inventory = invent.Items;
                Cursor.lockState = CursorLockMode.None;
                player.GetComponent<PlayerController>().canMove = false;
                for (int i = 0; i < inventory.Count; i++)
                {
                    bool found = false;
                    for (int j = 0; j < allItems.Count; j++)
                    {
                        if (inventory[i] != null && allItems[j] != null && allItems[j].enu == inventory[i].enu)
                        {
                            allItems[j].count += inventory[i].count;
                            found = true;
                        }
                    }
                    if (!found && inventory[i] != null)
                    {
                        allItems.Add(inventory[i]);
                    }
                }
                checkTools();
                checkRecipes();
            }
            else
            {
                for(int i = 0; i < slots.Length; i++)
                {
                    if (slots[i] != null)
                    {
                        Destroy(slots[i]);
                    }
                }
                for (int i = 0; i < slots.Length; i++)
                {
                    if (slots[i] != null)
                    {
                        Destroy(slots[i]);
                    }
                }
                for (int i = 0; i < toolSlots.Length; i++)
                {
                    if (toolSlots[i] != null)
                    {
                        Destroy(toolSlots[i]);
                    }
                }
                Cursor.lockState = CursorLockMode.Locked;
                player.GetComponent<PlayerController>().canMove = true;
                allItems.Clear();
                craftingUi.SetActive(false);
            }
            
            
        }
    }
    public void upgradeRepair(int toolIndex)
    {
        for (int i = 0; i < tools[toolIndex].recipeList.recipe.Length; i++)
        {
            bool found = false;
            for (int j = 0; j < invent.Items.Count; j++)
            {
                if (!found && invent.Items[j] != null && tools[toolIndex].recipeList.recipe[i].enu == invent.Items[j].enu)
                {
                    found = true;
                    invent.RemoveItemAtIndex(j, tools[toolIndex].recipeList.recipe[i].count);
                }
            }
        }
    }
    public void Make(int recipeIndex)
    {
        Item adding = new Item(recipes[recipeIndex].recipeList.madeName, recipes[recipeIndex].recipeList.description, recipes[recipeIndex].recipeList.icon, recipes[recipeIndex].recipeList.makeCount, recipes[recipeIndex].recipeList.itemEnum, recipes[recipeIndex].recipeList.worldPrefab);

        for (int i = 0; i < recipes[recipeIndex].recipeList.recipe.Length; i++)
        {
            bool found = false;
            for (int j = 0; j < invent.Items.Count; j++)
            {
                if(!found && invent.Items[j] != null && recipes[recipeIndex].recipeList.recipe[i].enu == invent.Items[j].enu)
                {
                    found = true;
                    invent.RemoveItemAtIndex(j, recipes[recipeIndex].recipeList.recipe[i].count);
                }
            }
        }
        invent.AddItem(adding);
    }
    private void checkTools()
    {
        for (int i = 0; i < tools.Length; i++)
        {
            bool makeable = true;
            bool[] recipeArr = new bool[tools[i].recipeList.recipe.Length];
            for (int j = 0; j < tools[i].recipeList.recipe.Length; j++)
            {
                recipeArr[j] = false;
                if (allItems.Count >= 1)
                {
                    for (int k = 0; k < allItems.Count; k++)
                    {
                        if ((allItems[k].enu == tools[i].recipeList.recipe[j].enu) && (allItems[k].count >= tools[i].recipeList.recipe[j].count))
                        {
                            recipeArr[j] = true;
                        }
                    }
                }
            }
            for (int n = 0; n < recipeArr.Length; n++)
            {
                if (recipeArr[n] == false)
                {
                    makeable = false;
                }
            }
            tools[i].found = makeable;
            if (makeable)
            {
                GameObject newSlot = Instantiate(uiSlot, container);
                toolSlots[i] = newSlot;
                if(tools[i].recipeList.itemEnum == ItemEnum.REPAIR)
                {
                    newSlot.GetComponent<craftingAdd>().damageUpgrade = 0;
                    newSlot.GetComponent<craftingAdd>().repair = true;
                    newSlot.GetComponent<craftingAdd>().upgrade = false;
                }
                else if(tools[i].recipeList.itemEnum == ItemEnum.UPGRADE)
                {
                    print(tools[i].recipeList.makeCount);
                    newSlot.GetComponent<craftingAdd>().damageUpgrade = tools[i].recipeList.makeCount;
                    newSlot.GetComponent<craftingAdd>().repair = false;
                    newSlot.GetComponent<craftingAdd>().upgrade = true;
                }
                newSlot.GetComponent<craftingAdd>().recipeToMake = i;
                newSlot.GetComponent<craftingAdd>().craftName = tools[i].recipeList.madeName;
                newSlot.GetComponent<craftingAdd>().description = tools[i].recipeList.description;
                newSlot.GetComponent<craftingAdd>().make = true;
                
                for (int n = 0; n < tools[i].recipeList.recipe.Length; n++)
                {
                    GameObject ingredient = Instantiate(text, newSlot.transform);
                    ingredient.GetComponent<TextMeshProUGUI>().text = tools[i].recipeList.recipe[n].Name + " " + tools[i].recipeList.recipe[n].count;
                }
            }
            else
            {
                GameObject newSlot = Instantiate(uiSlot, container);
                toolSlots[i] = newSlot;
                newSlot.GetComponent<craftingAdd>().recipeToMake = i;
                newSlot.GetComponent<craftingAdd>().damageUpgrade = 0;
                newSlot.GetComponent<craftingAdd>().craftName = tools[i].recipeList.madeName;
                newSlot.GetComponent<craftingAdd>().description = tools[i].recipeList.description;
                newSlot.GetComponent<craftingAdd>().make = false;
                newSlot.GetComponent<craftingAdd>().repair = false;
                newSlot.GetComponent<craftingAdd>().upgrade = false;
                for (int n = 0; n < tools[i].recipeList.recipe.Length; n++)
                {
                    GameObject ingredient = Instantiate(text, newSlot.transform);
                    ingredient.GetComponent<TextMeshProUGUI>().text = tools[i].recipeList.recipe[n].Name + " " + tools[i].recipeList.recipe[n].count;
                }
            }
        }
    }
    private void checkRecipes()
    {
        for (int i = 0; i < recipes.Length; i++)
        {
            bool makeable = true;
            bool[] recipeArr = new bool[recipes[i].recipeList.recipe.Length];
            for (int j = 0; j < recipes[i].recipeList.recipe.Length; j++)
            {
                recipeArr[j] = false;
                if (allItems.Count >= 1)
                {
                    for (int k = 0; k < allItems.Count; k++)
                    {
                        if ((allItems[k].enu == recipes[i].recipeList.recipe[j].enu) && (allItems[k].count >= recipes[i].recipeList.recipe[j].count))
                        {
                            recipeArr[j] = true;
                        }
                    }
                }
            }
            for (int n = 0; n < recipeArr.Length; n++)
            {
                if (recipeArr[n] == false)
                {
                    makeable = false;
                }
            }
            recipes[i].found = makeable;
            if (makeable)
            {
                GameObject newSlot = Instantiate(uiSlot, container);
                slots[i] = newSlot;
                newSlot.GetComponent<craftingAdd>().recipeToMake = i;
                newSlot.GetComponent<craftingAdd>().craftName = recipes[i].recipeList.madeName;
                newSlot.GetComponent<craftingAdd>().description = recipes[i].recipeList.description;
                newSlot.GetComponent<craftingAdd>().make = true;
                newSlot.GetComponent<craftingAdd>().damageUpgrade = 0;
                newSlot.GetComponent<craftingAdd>().repair = false;
                newSlot.GetComponent<craftingAdd>().upgrade = false;
                for (int n = 0; n < recipes[i].recipeList.recipe.Length; n++)
                {
                    GameObject ingredient = Instantiate(text, newSlot.transform);
                    ingredient.GetComponent<TextMeshProUGUI>().text = recipes[i].recipeList.recipe[n].Name + " " + recipes[i].recipeList.recipe[n].count;
                }
            }
            else
            {
                GameObject newSlot = Instantiate(uiSlot, container);
                slots[i] = newSlot;
                newSlot.GetComponent<craftingAdd>().recipeToMake = i;
                newSlot.GetComponent<craftingAdd>().craftName = recipes[i].recipeList.madeName;
                newSlot.GetComponent<craftingAdd>().description = recipes[i].recipeList.description;
                newSlot.GetComponent<craftingAdd>().make = false;
                newSlot.GetComponent<craftingAdd>().damageUpgrade = 0;
                newSlot.GetComponent<craftingAdd>().repair = false;
                newSlot.GetComponent<craftingAdd>().upgrade = false;
                for (int n = 0; n < recipes[i].recipeList.recipe.Length; n++)
                {
                    GameObject ingredient = Instantiate(text, newSlot.transform);
                    ingredient.GetComponent<TextMeshProUGUI>().text = recipes[i].recipeList.recipe[n].Name + " " + recipes[i].recipeList.recipe[n].count;
                }
            }
        }
    }
    public bool checkMade(int i)
    {
        bool makeable = true;
        bool[] recipeArr = new bool[recipes[i].recipeList.recipe.Length];
        for (int j = 0; j < recipes[i].recipeList.recipe.Length; j++)
        {
            recipeArr[j] = false;
            if (allItems.Count >= 1)
            {
                for (int k = 0; k < allItems.Count; k++)
                {
                    if ((allItems[k].enu == recipes[i].recipeList.recipe[j].enu) && (allItems[k].count >= recipes[i].recipeList.recipe[j].count))
                    {
                        recipeArr[j] = true;
                    }
                }
            }
        }
        for (int n = 0; n < recipeArr.Length; n++)
        {
            if (recipeArr[n] == false)
            {
                makeable = false;
            }
        }
        return makeable;
    }
}
[System.Serializable]
public class recipeNode
{
    public Recipe recipeList;
    public bool found = false;

}

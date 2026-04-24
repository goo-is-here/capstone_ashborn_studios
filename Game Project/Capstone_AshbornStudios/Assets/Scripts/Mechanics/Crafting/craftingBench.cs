using UnityEngine;
using System.Collections.Generic;
using TMPro;

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
    GameObject[] slots;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        invent = player.GetComponent<Inventory>();
        slots = new GameObject[recipes.Length];
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position) < 5f && Input.GetKeyDown(KeyCode.E))
        {
            craftingUi.SetActive(!craftingUi.activeSelf);
            if (craftingUi.activeSelf)
            {
                Cursor.lockState = CursorLockMode.None;
                player.GetComponent<PlayerController>().canMove = false;
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
                Cursor.lockState = CursorLockMode.Locked;
                player.GetComponent<PlayerController>().canMove = true;
            }
            allItems.Clear();
            List<Item> inventory = invent.Items;
            for(int i = 0; i < inventory.Count; i++)
            {
                bool found = false;
                for(int j = 0; j < allItems.Count; j++)
                {
                    if(inventory[i] != null && allItems[j] != null && allItems[j].enu == inventory[i].enu)
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
            for(int i = 0; i < recipes.Length; i++)
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
                            if((allItems[k].enu == recipes[i].recipeList.recipe[j].enu) && (allItems[k].count >= recipes[i].recipeList.recipe[j].count))
                            {
                                recipeArr[j] = true;
                            }
                        }
                    }
                }
                for(int n = 0; n < recipeArr.Length; n++)
                {
                    if(recipeArr[n] == false)
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
                    for(int n = 0; n < recipes[i].recipeList.recipe.Length; n++)
                    {
                        GameObject ingredient = Instantiate(text, newSlot.transform);
                        ingredient.GetComponent<TextMeshProUGUI>().text = recipes[i].recipeList.recipe[n].Name + " " + recipes[i].recipeList.recipe[n].count;
                    }
                }
            }
        }
    }
    public void Make(int recipeIndex)
    {
        Item adding = new Item(recipes[recipeIndex].recipeList.madeName, recipes[recipeIndex].recipeList.description, recipes[recipeIndex].recipeList.icon, recipes[recipeIndex].recipeList.makeCount, recipes[recipeIndex].recipeList.itemEnum, recipes[recipeIndex].recipeList.worldPrefab);
        bool breaking = false;
        for (int i = 0; i < recipes[recipeIndex].recipeList.recipe.Length; i++)
        {
            bool found = false;
            for (int j = 0; j < invent.Items.Count; j++)
            {
                if(!found && invent.Items[j] != null && recipes[recipeIndex].recipeList.recipe[i].enu == invent.Items[j].enu)
                {
                    if(invent.Items[j].count - recipes[recipeIndex].recipeList.recipe[i].count <= 0)
                    {
                        breaking = true;
                    }
                    found = true;
                    invent.RemoveItemAtIndex(j, recipes[recipeIndex].recipeList.recipe[i].count);
                }
            }
        }
        if (breaking)
        {
            Destroy(slots[recipeIndex]);
        }
        invent.AddItem(adding);
    }
}
[System.Serializable]
public class recipeNode
{
    public Recipe recipeList;
    public bool found = false;

}

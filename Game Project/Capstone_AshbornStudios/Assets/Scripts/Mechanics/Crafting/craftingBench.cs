using UnityEngine;
using System.Collections.Generic;

public class craftingBench : MonoBehaviour
{
    public GameObject player;
    public Inventory invent;
    public List<Item> allItems = new List<Item>();
    public recipeNode[] recipes;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        invent = player.GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position) < 5f && Input.GetKeyDown(KeyCode.E))
        {
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
            }
            print(recipes[0].found);
        }
    }
}
[System.Serializable]
public class recipeNode
{
    public Recipe recipeList;
    public bool found = false;

}

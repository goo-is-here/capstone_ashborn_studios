using UnityEngine;

public class toolUpgrade : MonoBehaviour
{
    [SerializeField] storeScript store;
    PlayerController player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    public void toolUpgradeAction()
    {
        store.currentTool++;
        if(store.currentTool < store.toolUpgrades.Count)
        {
            player.diggingRange += store.toolUpgrades[store.currentTool - 1].rangeIncrease;
            hideRecipe();
            showRecipe();
        }
        
    }
    GameObject[] slots;
    public void showRecipe()
    {
        if (store.currentTool < store.toolUpgrades.Count)
        {
            slots = new GameObject[store.inventoryUpgrade[store.currentInventory].recipe.Length];
            for (int i = 0; i < store.toolUpgrades[store.currentTool].recipe.Length; i++)
            {
                GameObject slot = Instantiate(store.recipeIngredient, store.recipeHolder.transform);
                string text = store.toolUpgrades[store.currentTool].recipe[i].Name + " " + store.toolUpgrades[store.currentTool].recipe[i].count;
                slots[i] = slot;
                slot.GetComponent<recipeHolder>().setText(text);
                slot.GetComponent<recipeHolder>().setImage(store.toolUpgrades[store.currentTool].icon);
            }
        }
        
    }
    public void hideRecipe()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            Destroy(slots[i]);
        }
    }

}

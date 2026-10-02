using UnityEngine;

public class objectUpgrade : MonoBehaviour
{
    [SerializeField] storeScript store;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void objectUnlockAction()
    {
        store.currentObject++;
        if(store.currentObject < store.objectsToUnlock.Count)
        {
            store.objectsToUnlock[store.currentObject - 1].SetActive(true);
            hideRecipe();
            showRecipe();
        }
    }
    GameObject[] slots;
    public void showRecipe()
    {
        if (store.currentObject < store.objectsToUnlock.Count)
        {
            slots = new GameObject[store.inventoryUpgrade[store.currentInventory].recipe.Length];
            for (int i = 0; i < store.objectsToUnlockRecipe[store.currentObject].recipe.Length; i++)
            {
                GameObject slot = Instantiate(store.recipeIngredient, store.recipeHolder.transform);
                slots[i] = slot;
                string text = store.objectsToUnlockRecipe[store.currentObject].recipe[i].Name + " " + store.objectsToUnlockRecipe[store.currentObject].recipe[i].count;
                slot.GetComponent<recipeHolder>().setText(text);
                slot.GetComponent<recipeHolder>().setImage(store.objectsToUnlockRecipe[store.currentObject].icon);
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

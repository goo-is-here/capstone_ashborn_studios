using UnityEngine;
using System.Collections;
using TMPro;
public class inventoryUpgrade : MonoBehaviour
{
    [SerializeField] storeScript store;
    characterInventory player;
    GameObject[] slots;
    TextMeshProUGUI moleText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moleText = store.moleText;
        player = GameObject.FindGameObjectWithTag("Inventory").GetComponent<characterInventory>();
    }

    // Update is called once per frame
    public void inventoryUpgradeAction()
    {
        store.currentInventory++;
        if (store.currentInventory < store.inventoryUpgrade.Count)
        {
            store.removeItems(store.inventoryUpgrade[store.currentInventory]);
            player.numSlots += store.inventoryUpgrade[store.currentInventory - 1].slotsToAdd;
            hideRecipe();
            showRecipe();
        }

    }
    public void showRecipe()
    {
        print(store.inventoryUpgrade[store.currentInventory].recipe.Length);
        if (store.currentInventory < store.inventoryUpgrade.Count)
        {
            StartCoroutine(talkingMole(store.inventoryUpgrade[store.currentInventory].moleComment));
            slots = new GameObject[store.inventoryUpgrade[store.currentInventory].recipe.Length];
            for (int i = 0; i < store.inventoryUpgrade[store.currentInventory].recipe.Length; i++)
            {
                GameObject slot = Instantiate(store.recipeIngredient, store.recipeHolder.transform);
                string text = store.inventoryUpgrade[store.currentInventory].recipe[i].Name + " " + store.inventoryUpgrade[store.currentInventory].recipe[i].count;
                slots[i] = slot;
                slot.GetComponent<recipeHolder>().setText(text);
                slot.GetComponent<recipeHolder>().setImage(store.inventoryUpgrade[store.currentInventory].icon);
            }
        }

    }
    public void hideRecipe()
    {
        StopAllCoroutines();
        for (int i = 0; i < slots.Length; i++)
        {
            Destroy(slots[i]);
        }
    }
    IEnumerator talkingMole(string text)
    {
        for (int i = 0; i < text.Length; i++)
        {
            moleText.text = moleText.text + text[i];
            yield return new WaitForSeconds(0.01f);
        }
    }

}

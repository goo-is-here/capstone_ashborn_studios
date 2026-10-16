using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
public class objectUpgrade : MonoBehaviour
{
    [SerializeField] storeScript store;
    TextMeshProUGUI moleText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moleText = store.moleText;
    }
    public void objectUnlockAction()
    {
        
        if(store.currentObject < store.objectsToUnlock.Count)
        {
            store.removeItems(store.objectsToUnlockRecipe[store.currentObject]);
            store.objectsToUnlock[store.currentObject].SetActive(true);
            hideRecipe();
            showRecipe();
            store.clearText();
            store.buyItem();
        }
        store.currentObject++;
    }
    GameObject[] slots;
    public void showRecipe()
    {
        if (store.currentObject < store.objectsToUnlock.Count)
        {
            StartCoroutine(talkingMole(store.objectsToUnlockRecipe[store.currentObject].moleComment));
            slots = new GameObject[store.objectsToUnlockRecipe[store.currentObject].recipe.Length];
            for (int i = 0; i < store.objectsToUnlockRecipe[store.currentObject].recipe.Length; i++)
            {
                GameObject slot = Instantiate(store.recipeIngredient, store.recipeHolder.transform);
                slots[i] = slot;
                string text = "" + store.objectsToUnlockRecipe[store.currentObject].recipe[i].count;
                slot.GetComponent<recipeHolder>().setText(text);
                slot.GetComponent<recipeHolder>().setImage(store.objectsToUnlockRecipe[store.currentObject].recipe[i].icon);
                
            }
        }
        if (!gameObject.GetComponent<Button>().interactable)
        {
            store.invalidAction();
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

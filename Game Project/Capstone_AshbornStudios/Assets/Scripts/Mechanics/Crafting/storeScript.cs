using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine.UI;
public class storeScript : MonoBehaviour
{
    public List<Recipe> toolUpgrades;
    public List<GameObject> objectsToUnlock;
    public List<Recipe> objectsToUnlockRecipe;
    public List<Recipe> inventoryUpgrade;
    public int currentTool;
    public int currentObject;
    public int currentInventory;
    Vector3 startPosition;
    [SerializeField] Transform swingAmount;
    public GameObject recipeHolder;
    Vector3 endPosition;
    [SerializeField] float swingSpeed; 
    [SerializeField] GameObject toolHolder;
    [SerializeField] GameObject objectHolder;
    [SerializeField] GameObject inventoryHolder;
    public GameObject recipeIngredient;
    public Camera storeCamera;
    public TextMeshProUGUI moleText;
    characterInventory playerInventory;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<characterInventory>();
        startPosition = recipeHolder.transform.position;
        endPosition = swingAmount.position;
        for(int i = 0; i < objectsToUnlock.Count; i++)
        {
            objectsToUnlock[i].SetActive(false);
        }
        recipeHolder.transform.position = endPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentTool >= toolUpgrades.Count && validateInventory(toolUpgrades[currentTool]))
        {
            toolHolder.GetComponent<Button>().interactable = true;
            toolHolder.GetComponent<toolUpgrade>().hideRecipe();
            toolHolder.SetActive(false);
        }
        else
        {
            toolHolder.GetComponent<Button>().interactable = false;
        }
        if (currentObject >= objectsToUnlock.Count && validateInventory(objectsToUnlockRecipe[currentObject]))
        {
            objectHolder.GetComponent<Button>().interactable = true;
            objectHolder.GetComponent<objectUpgrade>().hideRecipe();
            objectHolder.SetActive(false);
        }
        else
        {
            objectHolder.GetComponent<Button>().interactable = false;
        }
        if (currentInventory >= inventoryUpgrade.Count && validateInventory(inventoryUpgrade[currentInventory]))
        {
            inventoryHolder.GetComponent<Button>().interactable = true;
            inventoryHolder.GetComponent<inventoryUpgrade>().hideRecipe();
            inventoryHolder.SetActive(false);
        }
        else
        {
            objectHolder.GetComponent<Button>().interactable = false;
        }
    }
    public void swingOut()
    {
        StartCoroutine(LerpPosition(startPosition, endPosition, swingSpeed));
    }
    public void swingIn()
    {
        StartCoroutine(LerpPosition(endPosition, startPosition, swingSpeed));
        moleText.text = "";
    }
    IEnumerator LerpPosition(Vector3 targetPosition, Vector3 startPosition, float duration)
    {
        float time = 0;

        while (time < duration)
        {
            float t = time / duration;
            t = t * t * (3f - 2f * t);

            recipeHolder.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            time += Time.deltaTime;
            yield return null;
        }
        recipeHolder.transform.position = targetPosition;
    }
    bool validateInventory(Recipe recipe)
    {
        bool[] canMake = new bool[recipe.recipe.Length];

        for(int i = 0; i < recipe.recipe.Length; i++)
        {
            canMake[i] = false;
            foreach(Item ite in playerInventory.inventoryItemList)
            {
                if(!canMake[i] && ite.count >= recipe.recipe[i].count)
                {
                    canMake[i] = true;
                }
            }
        }
        bool tempBool = true;
        for(int i = 0; i < canMake.Length; i++)
        {
            if (!canMake[i])
            {
                tempBool = false;
            }
        }
        return tempBool;
    }
    void removeItems(Recipe recipe)
    {
        for (int i = 0; i < recipe.recipe.Length; i++)
        {
            playerInventory.removeItem(recipe.recipe[i].enu, recipe.recipe[i].count);
        }
    }
    
}

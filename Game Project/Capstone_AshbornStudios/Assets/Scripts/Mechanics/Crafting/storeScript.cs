using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine.UI;
public class storeScript : MonoBehaviour, IDataPersistence
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
    public characterInventory playerInventory;
    [SerializeField] AudioClip invalidBuy;
    [SerializeField] AudioClip[] buySounds;
    [SerializeField] AudioSource source;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
        if (currentTool >= 0 && currentTool < toolUpgrades.Count && validateInventory(toolUpgrades[currentTool]))
        {
            toolHolder.GetComponent<Button>().interactable = true;
        }
        else
        {
            toolHolder.GetComponent<Button>().interactable = false;
        }
        if (currentObject >= 0 && currentObject < objectsToUnlockRecipe.Count && validateInventory(objectsToUnlockRecipe[currentObject]))
        {
            objectHolder.GetComponent<Button>().interactable = true;
        }
        else
        {
            objectHolder.GetComponent<Button>().interactable = false;
        }

        if (currentInventory >= 0 && currentInventory < inventoryUpgrade.Count && validateInventory(inventoryUpgrade[currentInventory]))
        {
            inventoryHolder.GetComponent<Button>().interactable = true;
        }
        else
        {
            inventoryHolder.GetComponent<Button>().interactable = false;
        }
    }
    public void invalidAction()
    {
        if (!source.isPlaying)
        {
            source.PlayOneShot(invalidBuy);
        }
    }
    public void buyItem()
    {
        int sound = Random.Range(0, 1);
        if (!source.isPlaying)
        {
            source.PlayOneShot(buySounds[sound]);
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
    public void clearText()
    {
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
        if (recipe == null || recipe.recipe == null)
            return false;

        foreach (Node requiredItem in recipe.recipe)
        {
            if (requiredItem == null)
                return false;

            int totalCount = 0;

            foreach (Item inventoryItem in playerInventory.inventoryItemList)
            {
                if (inventoryItem != null && inventoryItem.enu == requiredItem.enu)
                {
                    totalCount += inventoryItem.count;
                }
            }

            if (totalCount < requiredItem.count)
                return false;
        }

        return true;
    }
    public void removeItems(Recipe recipe)
    {
        for (int i = 0; i < recipe.recipe.Length; i++)
        {
            playerInventory.removeItem(recipe.recipe[i].enu, recipe.recipe[i].count);
        }
    }
    public void LoadData(GameData data)
    {
        currentTool = data.tool;
        currentObject = data.objectNum;
        currentInventory = data.invent;
    }
    //save variables into game data
    //Currently saving current scene, position per scene, damage of tool, durability of tool, max durability
    public void SaveData(ref GameData data)
    {
        if (data != null)
        {
            data.tool = currentTool;
            data.objectNum = currentObject;
            data.invent = currentInventory;
        }

    }
}

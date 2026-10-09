using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;

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
        if(currentTool >= toolUpgrades.Count)
        {
            toolHolder.GetComponent<toolUpgrade>().hideRecipe();
            toolHolder.SetActive(false);
        }
        if (currentObject >= objectsToUnlock.Count)
        {
            objectHolder.GetComponent<objectUpgrade>().hideRecipe();
            objectHolder.SetActive(false);
        }
        if (currentInventory >= inventoryUpgrade.Count)
        {
            inventoryHolder.GetComponent<inventoryUpgrade>().hideRecipe();
            inventoryHolder.SetActive(false);
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

    
}

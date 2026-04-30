using UnityEngine;
using System.Collections;

public class growPlant : MonoBehaviour
{
    public float targetScale;
    public float timeToLerp = 0.25f;
    float scaleModifier = 1;
    Transform player;
    [SerializeField] string plantName;
    [SerializeField] string plantDesc;
    [SerializeField] Sprite plantImage;
    [SerializeField] GameObject plantPrefab;
    [SerializeField] growRoomEnter enter;
    bool canPickUp = true;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (enter.entered)
        {
            resetScale();
        }
    }
    private void Update()
    {
        Vector3 scaleTarget = new Vector3(1, 1, 1);
        if(Vector3.Distance(player.position, transform.position) < 2f && transform.localScale.x >= .2f && canPickUp)
        {
            canPickUp = false;
            //Food_Item plant = new Food_Item(plantName, plantDesc, plantImage, 1, ItemEnum.MOSSFOOD, plantPrefab, 50);
            //player.gameObject.GetComponent<Inventory>().AddItem(plant);
            Vector3 scale = new Vector3(0.9f, 0.9f, 0.9f);
            transform.localScale -= scale;
            StopCoroutine(LerpFunction(targetScale, timeToLerp));
            resetScale();
            
        }
    }
    public void resetScale()
    {
        transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        StartCoroutine(LerpFunction(targetScale, timeToLerp));
        if(transform.localScale.x >= .1f)
        {
            canPickUp = true;
        }
        
    }

    IEnumerator LerpFunction(float endValue, float duration)
    {
        float time = 0;
        float startValue = scaleModifier;
        Vector3 startScale = transform.localScale;

        while (time < duration)
        {
            scaleModifier = Mathf.Lerp(startValue, endValue, time / duration);
            transform.localScale = startScale * scaleModifier;
            time += Time.deltaTime;
            yield return null;
        }

        transform.localScale = startScale * endValue;
        scaleModifier = endValue;
    }
}

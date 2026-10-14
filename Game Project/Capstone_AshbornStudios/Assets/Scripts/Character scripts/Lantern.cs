using JetBrains.Annotations;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Lantern : MonoBehaviour, IDataPersistence
{
    private GameObject[] gems;
    private GameObject closestGem;
    public Material lanternMat;
    public float defaultDetectionDistance = 50f;
    public float detectionDistanceModifier = 1f;
    public GameObject player;

    public float defaultBrightness = 0.5f;

    public float minBrightness = 5f;
    public float maxBrightness = 20f;
    private Light pLight;
    private float brightnessRange;
    public bool lanternUnlocked = false;
    private bool gemsArrayInitialized = false;


    public void SaveData(ref GameData data)
    {
        data.lanternUnlocked = lanternUnlocked;
    }

    public void LoadData(GameData data)
    {
        lanternUnlocked = data.lanternUnlocked;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(DelayedStart());
        pLight = GetComponent<Light>();
        brightnessRange = maxBrightness - minBrightness; 
    }

    private IEnumerator DelayedStart()
    {
        yield return null;
        yield return null;
        yield return null;
        gems = GameObject.FindGameObjectsWithTag("Gems");
        gemsArrayInitialized = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gemsArrayInitialized)
        {
            return;

        }

        if (gems.Length == 0)
        {
            return;
        }

        closestGem = gems[0];
        foreach(GameObject gem in gems)
        {
            int index = 0;
            if (Vector3.Distance(transform.position, gem.transform.position) < Vector3.Distance(transform.position, closestGem.transform.position) || closestGem.GetComponent<gem>().collected == true)
            {
                closestGem = gem;
            }
            index += 1;
        }
        
        if(closestGem.GetComponent<gem>().collected != true)
        {
            float distanceFromGem = Vector3.Distance(transform.position, closestGem.transform.position);
            if (distanceFromGem < defaultDetectionDistance * detectionDistanceModifier)
            {
                //print("GemDetected");
            }
            float newMult = 0f;

            newMult = (distanceFromGem / -detectionDistanceModifier) + defaultDetectionDistance;
            newMult = Mathf.Clamp(newMult, 0, defaultDetectionDistance);
            float finalValue = (newMult / defaultDetectionDistance);
            
            lanternMat.SetFloat("_Mult", finalValue + defaultBrightness);
            pLight.intensity = (finalValue * brightnessRange) + minBrightness;
        }
        else
        {
            lanternMat.SetFloat("_Mult", defaultBrightness);
        }


    }
}

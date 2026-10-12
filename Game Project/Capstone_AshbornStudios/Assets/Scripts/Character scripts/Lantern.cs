using System.Collections;
using UnityEngine;

public class Lantern : MonoBehaviour
{
    private GameObject[] gems;
    private GameObject closestGem;
    public Material lanternMat;
    public float defaultDetectionDistance = 50f;
    public float detectionDistanceModifier = 1f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(DelayedStart());
    }

    private IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(1.0f);
        gems = GameObject.FindGameObjectsWithTag("Gems");
    }

    // Update is called once per frame
    void Update()
    {
        closestGem = gems[0];
        foreach(GameObject gem in gems)
        {
            if(Vector3.Distance(transform.position, gem.transform.position) < Vector3.Distance(transform.position, closestGem.transform.position))
            {
                closestGem = gem;
            }
        }

        float distanceFromGem = Vector3.Distance(transform.position, closestGem.transform.position);
        float newMult = 0f;

        newMult = (distanceFromGem / -detectionDistanceModifier) + defaultDetectionDistance;
        newMult = Mathf.Clamp(newMult, 0, defaultDetectionDistance);
        float finalValue = newMult / defaultDetectionDistance;

        lanternMat.SetFloat("_Mult", finalValue);
    }
}

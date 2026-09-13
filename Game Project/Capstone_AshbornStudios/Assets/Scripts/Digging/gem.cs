using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class gem : MonoBehaviour
{
    public GameObject gemSpawner;
    public int gemID;

    void OnTriggerEnter(Collider other)
    {
       gemSpawner.GetComponent<SpawnGems>().setGemAsCollected(gemID);
       Destroy(gameObject);
    }
}

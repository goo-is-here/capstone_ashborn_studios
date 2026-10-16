using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class gem : MonoBehaviour
{
    public GameObject gemSpawner;
    public int gemID;
    [HideInInspector] public bool collected = false;
    private MeshRenderer mesh;
    private BoxCollider collision;
    private AudioSource audio;

    private void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        collision = GetComponent<BoxCollider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            gemSpawner.GetComponent<SpawnGems>().setGemAsCollected(gemID);
            other.GetComponent<PlayerController>().CollectGem(gemID);
            Collect();
        }
    }

    public void Collect()
    {
        mesh.enabled = false;
        collision.enabled = false;
        collected = true;
    }
}

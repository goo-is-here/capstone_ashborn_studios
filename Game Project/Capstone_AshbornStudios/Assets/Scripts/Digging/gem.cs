using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class gem : MonoBehaviour
{
    //refrences
    public GameObject gemSpawner;
    public AudioClip audioClip;
    public Sprite icon;

    //used for gemrefrences across seperate scripts
    public int gemID;

    [HideInInspector] public bool collected = false;
    private MeshRenderer mesh;
    private BoxCollider collision;
    private AudioSource audioSource;


    private void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        collision = GetComponent<BoxCollider>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            gemSpawner.GetComponent<SpawnGems>().setGemAsCollected(gemID);
            other.GetComponent<PlayerController>().CollectGem(gemID);
            audioSource.PlayOneShot(audioClip);
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

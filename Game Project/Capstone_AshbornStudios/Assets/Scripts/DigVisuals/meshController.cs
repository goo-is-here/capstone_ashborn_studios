using UnityEngine;
using System.Collections;

public class meshController : MonoBehaviour
{
    private MeshFilter meshFilter;
    private MeshCollider meshCollider;
    [SerializeField] Camera cam;
    [SerializeField] LayerMask terrainLayer;
    [SerializeField] Transform player;
    PlayerController cont;
    bool canMine = true;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        cont = player.GetComponent<PlayerController>();
        cam = Camera.main;
        meshFilter = GetComponent<MeshFilter>();
        meshCollider = GetComponent<MeshCollider>();
    }
   
}

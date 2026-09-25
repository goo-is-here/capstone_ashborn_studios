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
    private void Update()
    {
       if(Input.GetMouseButton(0) && canMine)
        {
            StartCoroutine(mineCooldown());
        }
    }

    private Mesh mesh;
    private Vector3[] vertices;
    private IEnumerator mineCooldown()
    {
        canMine = false;
        yield return new WaitForSeconds(cont.mineSpeed / 2);
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if ( Physics.Raycast(ray, out hitInfo, cont.diggingRange, terrainLayer))
        {
            terraform(hitInfo.point, cont.damageVal, cont.diggingRange);
        }
        yield return new WaitForSeconds(cont.mineSpeed / 2);
        canMine = true;
    }
    private void terraform(Vector3 position, float height, float range)
    {
        mesh = meshFilter.sharedMesh;
        vertices = mesh.vertices;
        position -= meshFilter.transform.position;

        int i = 0;
        foreach(Vector3 vert in vertices)
        {
            if(Vector3.Distance(vert, position) <= range)
            {
                vertices[i] = vert + cam.transform.forward * height;
            }
            i++;
        }
        mesh.vertices = vertices;
        meshFilter.mesh = mesh;
        meshCollider.sharedMesh = mesh;
    }
}

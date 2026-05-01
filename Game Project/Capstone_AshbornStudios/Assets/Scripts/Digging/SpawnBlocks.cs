using UnityEngine;
using System.Collections;
using TMPro;
[RequireComponent(typeof(diggableBlock))]
public class SpawnBlocks : MonoBehaviour
{
    Vector3[] vertices;
    Mesh mesh;
    GameObject player;
    GameObject block;
    Vector3 startPos;
    public float spawnCheck = .5f;
    public bool[] spawnedBlocks = { false, false, false, false, false, false };
    private MeshRenderer bounds;
    Vector3 size;
    public bool doSpawnNeighbors = true;
    public int TESTINGONLY = 0;
    PlayerController controller;
    blockType blockEnum;
    setBlock setter;
    bool setYet = false;
    bool isBeingDestroyed = false;
    public GameObject root;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        block = this.gameObject;
        block.GetComponent<diggableBlock>().enabled = true;
        blockEnum = blockType.CLEARSTONE;
        for (int i = 0; i < 6; i++)
        {
            spawnedBlocks[i] = false;
        }
        startPos = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
        controller = player.GetComponent<PlayerController>();
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        mesh = meshFilter.mesh;
        vertices = mesh.vertices;
        bounds = GetComponent<MeshRenderer>();
        GameObject setterObject = GameObject.FindGameObjectWithTag("SetBlock");
        if (setterObject != null)
        {
            setter = setterObject.GetComponent<setBlock>();
        }

        Setter();
    }
    private void Setter()
    {
        if (!setYet)
        {
            setter.setTheBlock(transform.position, bounds, this, gameObject.GetComponent<diggableBlock>());
            setYet = true;
        }
    }
    public void spawnNeighbors()
    {
        size = bounds.bounds.size;
        if (Vector3.Distance(transform.position, player.transform.position) < 20f)
        {
            Vector3 leftCheck = new Vector3(transform.position.x + size.x, transform.position.y, transform.position.z);
            Collider[] intersecting = Physics.OverlapSphere(leftCheck, spawnCheck);
            if (!setter.blockPosition.Contains(leftCheck))
            {
                if (!spawnedBlocks[0])
                {
                    if (intersecting.Length == 0)
                    {
                        Instantiate(block, leftCheck, Quaternion.identity, transform.parent);
                    }
                    spawnedBlocks[0] = true;
                }
            }
            Vector3 rightCheck = new Vector3(transform.position.x - size.x, transform.position.y, transform.position.z);
            intersecting = Physics.OverlapSphere(rightCheck, spawnCheck);
            if (!setter.blockPosition.Contains(rightCheck))
            {
                if (!spawnedBlocks[1])
                {
                    if (intersecting.Length == 0)
                    {
                        Instantiate(block, rightCheck, Quaternion.identity, transform.parent);
                    }
                    spawnedBlocks[1] = true;
                }
            }
            Vector3 frontCheck = new Vector3(transform.position.x, transform.position.y, transform.position.z + size.z);
            intersecting = Physics.OverlapSphere(frontCheck, spawnCheck);
            if (!setter.blockPosition.Contains(frontCheck))
            {
                if (!spawnedBlocks[2])
                {
                    if (intersecting.Length == 0)
                    {
                        Instantiate(block, frontCheck, Quaternion.identity, transform.parent);
                    }
                    spawnedBlocks[2] = true;
                }
            }
            Vector3 backCheck = new Vector3(transform.position.x, transform.position.y, transform.position.z - size.z);
            intersecting = Physics.OverlapSphere(backCheck, spawnCheck);
            if (!setter.blockPosition.Contains(backCheck))
            {
                if (!spawnedBlocks[3])
                {
                    if (intersecting.Length == 0)
                    {
                        Instantiate(block, backCheck, Quaternion.identity, transform.parent);
                    }
                    spawnedBlocks[3] = true;
                }
            }
            Vector3 upCheck = new Vector3(transform.position.x, transform.position.y + size.y, transform.position.z);
            intersecting = Physics.OverlapSphere(upCheck, spawnCheck);
            if (!setter.blockPosition.Contains(upCheck))
            {
                if (!spawnedBlocks[4])
                {
                    if (intersecting.Length == 0)
                    {
                        Instantiate(block, upCheck, Quaternion.identity, transform.parent);
                    }
                    spawnedBlocks[4] = true;
                }
            }
            Vector3 downCheck = new Vector3(transform.position.x, transform.position.y - size.y, transform.position.z);
            if (!setter.blockPosition.Contains(downCheck))
            {
                intersecting = Physics.OverlapSphere(downCheck, spawnCheck);
                if (!spawnedBlocks[5])
                {
                    if (intersecting.Length == 0)
                    {
                        Instantiate(block, downCheck, Quaternion.identity, transform.parent);
                    }
                    spawnedBlocks[5] = true;
                }
            }
            
        }
        if (setter.blockPosition.Contains(transform.position))
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        
        bool done = true;
        if (Vector3.Distance(transform.position, player.transform.position) < 20f)
        {
            spawnNeighbors();
        }
        for(int i = 0; i < 6; i++)
        {
            if (!spawnedBlocks[i])
            {
                done = false;
            }
        }
        if (done)
        {
            this.enabled = false;
        }
    }
    public void resetState()
    {
        for (int i = 0; i < 6; i++)
        {
            spawnedBlocks[i] = false;
        }
    }
    public void setBlock(blockType enu)
    {
        blockEnum = enu;
    }
    public void setAir()
    {
        if (isBeingDestroyed) return;
        isBeingDestroyed = true;
        Destroy(gameObject);
    }
}

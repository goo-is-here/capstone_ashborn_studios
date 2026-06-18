using UnityEngine;
using System.Collections;
using TMPro;
[RequireComponent(typeof(diggableBlock))]
public class SpawnBlocks : MonoBehaviour
{
    //general block information
    Vector3[] vertices;
    Mesh mesh;
    GameObject player;
    GameObject block;
    Vector3 startPos;
    private MeshRenderer bounds;
    Vector3 size;
    blockType blockEnum;
    //sets the general block information
    setBlock setter;
    [Header("Spawning neighbor variables")]
    public float spawnCheck = .5f;
    public bool[] spawnedBlocks = { false, false, false, false, false, false };
    public bool doSpawnNeighbors = true;
    //used to make sure that even if you destroy every block around one that hasn't been broken it will still spawn
    bool setYet = false;
    bool isBeingDestroyed = false;
    public GameObject root;


    void Start()
    {
        //gets the game object and makes sure the diggable block script is active
        block = this.gameObject;
        block.GetComponent<diggableBlock>().enabled = true;
        //sets a temp enum
        blockEnum = blockType.CLEARSTONE;
        //resets the neighbors to false because it is spawned from a prefab
        for (int i = 0; i < 6; i++)
        {
            spawnedBlocks[i] = false;
        }
        //gets position
        startPos = transform.position;
        //gets player to reference for distance from block
        player = GameObject.FindGameObjectWithTag("Player");
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        mesh = meshFilter.mesh;
        vertices = mesh.vertices;
        bounds = GetComponent<MeshRenderer>();
        //gets the block setter and makes sure it's not null
        GameObject setterObject = GameObject.FindGameObjectWithTag("SetBlock");
        if (setterObject != null)
        {
            setter = setterObject.GetComponent<setBlock>();
        }
        //sets the block variables
        Setter();
    }
    //sets the block using the perlin noise function in set block, then once set do not re-set it
    private void Setter()
    {
        if (!setYet)
        {
            setter.setTheBlock(transform.position, bounds, this, gameObject.GetComponent<diggableBlock>());
            setYet = true;
        }
    }
    //spawns the six blocks around this block
    public void spawnNeighbors()
    {
        //gets the size of the block
        size = bounds.bounds.size;
        //if the player is close enough, begin
        if (Vector3.Distance(transform.position, player.transform.position) < 20f)
        {
            //sets a vector to the left of the block at a set distance
            Vector3 leftCheck = new Vector3(transform.position.x + size.x, transform.position.y, transform.position.z);
            //makes a sphere to check intersections, will grab every item that intersects
            Collider[] intersecting = Physics.OverlapSphere(leftCheck, spawnCheck);
            //gets the list of blocks already mined, if the block attempting to be spawned has already been mined, don't
            if (!setter.blockPosition.Contains(leftCheck))
            {
                //makes sure this has not already been attempted
                if (!spawnedBlocks[0])
                {
                    //checks to make sure OTHER blocks did not already spawn it
                    if (intersecting.Length == 0)
                    {
                        Instantiate(block, leftCheck, Quaternion.identity, transform.parent);
                    }
                    //set it to true either way
                    spawnedBlocks[0] = true;
                }
            }
            //repeat for right
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
            //repeat for front
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
            //repeat for back
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
            //repeat for up
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
            //repeat for down
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
        //if this block has already been mined previously, breaks this block but only AFTER spawning it's neighbors
        if (setter.blockPosition.Contains(transform.position))
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        //assumes all neighbors have been spawned
        bool done = true;
        //if the player is within 20 units spawn its neighbors
        if (Vector3.Distance(transform.position, player.transform.position) < 20f)
        {
            spawnNeighbors();
        }
        //checks all neighbors array, if any are not spawned the block is not considered done
        for(int i = 0; i < 6; i++)
        {
            if (!spawnedBlocks[i])
            {
                done = false;
            }
        }
        //disables a completely finished block to save resources
        if (done)
        {
            this.enabled = false;
        }
    }
    //resets neighbor states
    public void resetState()
    {
        for (int i = 0; i < 6; i++)
        {
            spawnedBlocks[i] = false;
        }
    }
    //sets the block type enum, used for things like spawning the drops or inventory
    public void setBlock(blockType enu)
    {
        blockEnum = enu;
    }
    //destroys blocks that have been broken previously or if should be spawned as air
    public void setAir()
    {
        if (isBeingDestroyed) return;
        isBeingDestroyed = true;
        Destroy(gameObject);
    }
}

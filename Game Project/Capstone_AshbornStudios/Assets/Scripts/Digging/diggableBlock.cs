using UnityEngine;
using System.Collections;
using TMPro;

public class diggableBlock : MonoBehaviour
{
    Vector3[] vertices;
    Mesh mesh;
    GameObject player;
    public GameObject block;
    float digRange = 2f;
    float blockHealth = 100f;
    float minDamage;
    public float currHealth;
    Vector3 startPos;
    public float spawnCheck = .5f;
    public bool[] spawnedBlocks = { false, false, false, false, false, false };
    private MeshRenderer bounds;
    Vector3 size;
    public bool doSpawnNeighbors = true;
    public enum blockType {DIRTSTONE, CLAY, CRUMBSTONE, LEMSTONE, ROOTSTONE, GRAVESTONE, CLEARSTONE};
    public int TESTINGONLY = 0;
    PlayerController controller;
    GameObject particles;
    GameObject dropped;
    blockType blockEnum;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        blockEnum = blockType.CLEARSTONE;
        /*outlineOn = new MaterialPropertyBlock();
        outlineOn.SetFloat("_outlineScale", 1.01f);
        outlineOff = new MaterialPropertyBlock();
        outlineOff.SetFloat("_outlineScale", 0.0f);*/
        for(int i = 0; i < 6; i++)
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
        if(setterObject != null)
        {
            setBlock setter = setterObject.GetComponent<setBlock>();
            setter.setTheBlock(transform.position.y, bounds, this);
        }
        
        if (doSpawnNeighbors)
        {
            spawnNeighbors();
        }
         currHealth = blockHealth;
    }
    void spawnNeighbors()
    {
        size = bounds.bounds.size;
        if (Vector3.Distance(transform.position, player.transform.position) < 20f)
        {
            Vector3 leftCheck = new Vector3(transform.position.x + size.x, transform.position.y, transform.position.z);
            Collider[] intersecting = Physics.OverlapSphere(leftCheck, spawnCheck);
            if (!spawnedBlocks[0])
            {
                if (intersecting.Length == 0)
                {
                    Instantiate(block, leftCheck, Quaternion.identity, transform.parent);
                }
                spawnedBlocks[0] = true;
            }
            Vector3 rightCheck = new Vector3(transform.position.x - size.x, transform.position.y, transform.position.z);
            intersecting = Physics.OverlapSphere(rightCheck, spawnCheck);
            if (!spawnedBlocks[1])
            {
                if (intersecting.Length == 0)
                {
                    Instantiate(block, rightCheck, Quaternion.identity, transform.parent);
                }
                spawnedBlocks[1] = true;
            }
            Vector3 frontCheck = new Vector3(transform.position.x, transform.position.y, transform.position.z + size.z);
            intersecting = Physics.OverlapSphere(frontCheck, spawnCheck);
            if (!spawnedBlocks[2])
            {
                if (intersecting.Length == 0)
                {
                    Instantiate(block, frontCheck, Quaternion.identity, transform.parent);
                }
                spawnedBlocks[2] = true;
            }
            Vector3 backCheck = new Vector3(transform.position.x, transform.position.y, transform.position.z - size.z);
            intersecting = Physics.OverlapSphere(backCheck, spawnCheck);
            if (!spawnedBlocks[3])
            {
                if (intersecting.Length == 0)
                {
                    Instantiate(block, backCheck, Quaternion.identity, transform.parent);
                }
                spawnedBlocks[3] = true;
            }
            Vector3 upCheck = new Vector3(transform.position.x, transform.position.y + size.y, transform.position.z);
            intersecting = Physics.OverlapSphere(upCheck, spawnCheck);
            if (!spawnedBlocks[4])
            {
                if (intersecting.Length == 0)
                {
                    Instantiate(block, upCheck, Quaternion.identity, transform.parent);
                }
                spawnedBlocks[4] = true;
            }
            Vector3 downCheck = new Vector3(transform.position.x, transform.position.y - size.y, transform.position.z);
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
    private void Update()
    {
        if(doSpawnNeighbors && Vector3.Distance(player.transform.position, transform.position) < 20)
        {
            spawnNeighbors();
        }
    }
    public void hitBlock(float damageVal, Vector3 position)
    {
        if(damageVal >= minDamage && controller.getDur() > 0)
        {
            currHealth -= damageVal;
            // moveVertices(position);
            //blockMaterials[0].color = new Color(1f - (currHealth / blockHealth), 1f + (currHealth / blockHealth), 0);
            //StartCoroutine(shimmy());
            controller.durabilityChange(-1f);
            if (currHealth <= 0)
            {
                this.GetComponent<MeshRenderer>().enabled = false;
                this.GetComponent<BoxCollider>().enabled = false;
                Vector3 pos = this.transform.position;
                GameObject part = Instantiate(particles, pos, Quaternion.identity, transform.parent);
                GameObject drop = Instantiate(dropped, pos, Quaternion.identity, transform.parent);
                Destroy(part, 3f);
                Destroy(drop, 10f);
                Destroy(gameObject);
            }
        }
        else if(controller.getDur() <= 0)
        {
            string print = "Gah! My tool is in need of repair!";
            controller.printText(print);
        }
        else
        {
            string print = "Gah! I need an upgrade that does " + minDamage + " to break this block!";
            controller.printText(print);
        }
    }
    public void resetState()
    {
        for(int i = 0; i < 6; i++)
        {
            spawnedBlocks[i] = false;
        }
    }
    public void setBlock(blockType enu, float health, float minDam, GameObject drop, GameObject part)
    {
        blockEnum = enu;
        blockHealth = health;
        minDamage = minDam;
        dropped = drop;
        particles = part;
    }
}

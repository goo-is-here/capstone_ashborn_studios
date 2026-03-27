using UnityEngine;
using System.Collections;
using TMPro;

public class diggableBlock : MonoBehaviour
{
    Vector3[] vertices;
    Mesh mesh;
    GameObject player;
    public GameObject block;
    public float digRange = 2f;
    public float blockHealth = 100f;
    float minDamage;
    public float currHealth;
    Vector3 startPos;
    public float moveAmount;
    public float moveSpeed;
    MaterialPropertyBlock outlineOn;
    MaterialPropertyBlock outlineOff;
    public float spawnCheck = .5f;
    bool[] spawnedBlocks = { false, false, false, false, false, false };
    private MeshRenderer bounds;
    Vector3 size;
    public bool doSpawnNeighbors = true;
    public enum blockType {DIRTSTONE, CLAY, CRUMBSTONE, LEMSTONE, ROOTSTONE, GRAVESTONE, CLEARSTONE};
    public int TESTINGONLY = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        blockType block = blockType.CLEARSTONE;
        /*outlineOn = new MaterialPropertyBlock();
        outlineOn.SetFloat("_outlineScale", 1.01f);
        outlineOff = new MaterialPropertyBlock();
        outlineOff.SetFloat("_outlineScale", 0.0f);*/
        
        startPos = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        mesh = meshFilter.mesh;
        vertices = mesh.vertices;
        bounds = GetComponent<MeshRenderer>();
        if (doSpawnNeighbors)
        {
            spawnNeighbors();
        }
        switch (TESTINGONLY)
        {
            case 1:
                block = blockType.CLAY;
                break;
            case 2:
                block = blockType.CRUMBSTONE;
                break;
            case 3:
                block = blockType.LEMSTONE;
                break;
            case 4:
                block = blockType.ROOTSTONE;
                break;
            case 5:
                block = blockType.GRAVESTONE;
                break;
            case 6:
                block = blockType.CLEARSTONE;
                break;
            default:
                block = blockType.DIRTSTONE;
                break;

        }
        print(block);
        switch (block)
        {
            case blockType.DIRTSTONE:
                blockHealth = 100;
                minDamage = 0;
                break;
            case blockType.CLAY:
                blockHealth = 100;
                minDamage = 0;
                break;
            case blockType.CRUMBSTONE:
                blockHealth = 130;
                minDamage = 10;
                break;
            case blockType.LEMSTONE:
                blockHealth = 150;
                minDamage = 20;
                break;
            case blockType.ROOTSTONE:
                blockHealth = 200;
                minDamage = 35;
                break;
            case blockType.GRAVESTONE:
                blockHealth = 300;
                minDamage = 50;
                break;
            case blockType.CLEARSTONE:
                blockHealth = 600;
                minDamage = 60;
                break;
        }
         currHealth = blockHealth;
    }
    void spawnNeighbors()
    {
        size = bounds.bounds.size;
        if (Vector3.Distance(transform.position, player.transform.position) < 50f)
        {
            Vector3 leftCheck = new Vector3(transform.position.x + size.x, transform.position.y, transform.position.z);
            Collider[] intersecting = Physics.OverlapSphere(leftCheck, spawnCheck);
            if (!spawnedBlocks[0] && intersecting.Length == 0)
            {
                Instantiate(block, leftCheck, Quaternion.identity, transform.parent);
                spawnedBlocks[0] = true;
            }
            Vector3 rightCheck = new Vector3(transform.position.x - size.x, transform.position.y, transform.position.z);
            intersecting = Physics.OverlapSphere(rightCheck, spawnCheck);
            if (!spawnedBlocks[1] && intersecting.Length == 0)
            {
                Instantiate(block, rightCheck, Quaternion.identity, transform.parent);
                spawnedBlocks[1] = true;
            }
            Vector3 frontCheck = new Vector3(transform.position.x, transform.position.y, transform.position.z + size.z);
            intersecting = Physics.OverlapSphere(frontCheck, spawnCheck);
            if (!spawnedBlocks[2] && intersecting.Length == 0)
            {
                Instantiate(block, frontCheck, Quaternion.identity, transform.parent);
                spawnedBlocks[2] = true;
            }
            Vector3 backCheck = new Vector3(transform.position.x, transform.position.y, transform.position.z - size.z);
            intersecting = Physics.OverlapSphere(backCheck, spawnCheck);
            if (!spawnedBlocks[3] && intersecting.Length == 0)
            {
                Instantiate(block, backCheck, Quaternion.identity, transform.parent);
                spawnedBlocks[3] = true;
            }
            Vector3 upCheck = new Vector3(transform.position.x, transform.position.y + size.y, transform.position.z);
            intersecting = Physics.OverlapSphere(upCheck, spawnCheck);
            if (!spawnedBlocks[4] && intersecting.Length == 0)
            {
                Instantiate(block, upCheck, Quaternion.identity, transform.parent);
                spawnedBlocks[4] = true;
            }
            Vector3 downCheck = new Vector3(transform.position.x, transform.position.y - size.y, transform.position.z);
            intersecting = Physics.OverlapSphere(downCheck, spawnCheck);
            if (!spawnedBlocks[5] && intersecting.Length == 0)
            {
                Instantiate(block, downCheck, Quaternion.identity, transform.parent);
                spawnedBlocks[5] = true;
            }
        }
    }
    private void Update()
    {
        /*
        if(Vector3.Distance(player.transform.position, transform.position) > 10)
        {
            this.GetComponent<MeshRenderer>().SetPropertyBlock(outlineOff, 1);
        }
        else
        {
            this.GetComponent<MeshRenderer>().SetPropertyBlock(outlineOn, 1);
        }
        */
    }
    public void hitBlock(float damageVal, Vector3 position)
    {
        if(damageVal >= minDamage)
        {
            currHealth -= damageVal;
            // moveVertices(position);
            //blockMaterials[0].color = new Color(1f - (currHealth / blockHealth), 1f + (currHealth / blockHealth), 0);
            //StartCoroutine(shimmy());
            if (currHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            player.GetComponent<PlayerController>().damageWeak(minDamage);
        }
    }
    IEnumerator shimmy()
    {
        transform.position = Vector3.Lerp(startPos, new Vector3(startPos.x + moveAmount, startPos.y, startPos.z), moveSpeed);
        yield return new WaitForSeconds(.1f);
        transform.position = Vector3.Lerp(transform.position, new Vector3(startPos.x - moveAmount*2.5f, startPos.y , startPos.z), moveSpeed);
        yield return new WaitForSeconds(.1f);
        transform.position = Vector3.Lerp(transform.position, startPos, moveSpeed);
        
    }
    /*
    void moveVertices(Vector3 position)
    {
        print("oops");
        for (int i = 0; i < vertices.Length; i++)
        {
            if (Vector3.Distance(position, transform.TransformPoint(vertices[i])) < digRange)
            {

                vertices[i] += Vector3.up * Time.deltaTime;
                /*
                var direction = transform.TransformPoint(vertices[i]) - position;
                //print("up" + (vertices[i] += Vector3.up));
                print(direction.normalized * 0.01f);
                vertices[i] += direction.normalized * 0.01f;
                
            }

        }
        mesh.vertices = vertices;
        mesh.RecalculateBounds();
    }
    */
    public void resetState()
    {
        for(int i = 0; i < 6; i++)
        {
            spawnedBlocks[i] = false;
        }
    }
}

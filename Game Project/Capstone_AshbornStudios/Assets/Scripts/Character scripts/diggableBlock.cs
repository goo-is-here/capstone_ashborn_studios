using UnityEngine;
using System.Collections;

public class diggableBlock : MonoBehaviour
{
    Vector3[] vertices;
    Mesh mesh;
    GameObject player;
    public GameObject block;
    public float digRange = 2f;
    public float blockHealth = 100f;
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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        /*outlineOn = new MaterialPropertyBlock();
        outlineOn.SetFloat("_outlineScale", 1.01f);
        outlineOff = new MaterialPropertyBlock();
        outlineOff.SetFloat("_outlineScale", 0.0f);*/
        currHealth = blockHealth;
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

    }
    void spawnNeighbors()
    {
        size = bounds.bounds.size;
        
        //check left
        if (Vector3.Distance(transform.position, player.transform.position) < 50f)
        {
            Vector3 leftCheck = new Vector3(transform.position.x + size.x, transform.position.y, transform.position.z);
            Collider[] intersecting = Physics.OverlapSphere(leftCheck, spawnCheck);
            if (intersecting.Length == 0)
            {
                Instantiate(block, leftCheck, Quaternion.identity, transform.parent);
            }
            Vector3 rightCheck = new Vector3(transform.position.x - size.x, transform.position.y, transform.position.z);
            intersecting = Physics.OverlapSphere(rightCheck, spawnCheck);
            if (intersecting.Length == 0)
            {
                Instantiate(block, rightCheck, Quaternion.identity, transform.parent);
            }
            Vector3 frontCheck = new Vector3(transform.position.x, transform.position.y, transform.position.z + size.z);
            intersecting = Physics.OverlapSphere(frontCheck, spawnCheck);
            if (intersecting.Length == 0)
            {
                Instantiate(block, frontCheck, Quaternion.identity, transform.parent);
            }
            Vector3 backCheck = new Vector3(transform.position.x, transform.position.y, transform.position.z - size.z);
            intersecting = Physics.OverlapSphere(backCheck, spawnCheck);
            if (intersecting.Length == 0)
            {
                Instantiate(block, backCheck, Quaternion.identity, transform.parent);
            }
            Vector3 upCheck = new Vector3(transform.position.x, transform.position.y + size.y, transform.position.z);
            intersecting = Physics.OverlapSphere(upCheck, spawnCheck);
            if (intersecting.Length == 0)
            {
                Instantiate(block, upCheck, Quaternion.identity, transform.parent);
            }
            Vector3 downCheck = new Vector3(transform.position.x, transform.position.y - size.y, transform.position.z);
            intersecting = Physics.OverlapSphere(downCheck, spawnCheck);
            if (intersecting.Length == 0)
            {
                Instantiate(block, downCheck, Quaternion.identity, transform.parent);
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
        currHealth -= damageVal;
        moveVertices(position);
        //blockMaterials[0].color = new Color(1f - (currHealth / blockHealth), 1f + (currHealth / blockHealth), 0);
        //StartCoroutine(shimmy());
    }
    IEnumerator shimmy()
    {
        transform.position = Vector3.Lerp(startPos, new Vector3(startPos.x + moveAmount, startPos.y, startPos.z), moveSpeed);
        yield return new WaitForSeconds(.1f);
        transform.position = Vector3.Lerp(transform.position, new Vector3(startPos.x - moveAmount*2.5f, startPos.y , startPos.z), moveSpeed);
        yield return new WaitForSeconds(.1f);
        transform.position = Vector3.Lerp(transform.position, startPos, moveSpeed);
        if(currHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
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
                */
            }

        }
        mesh.vertices = vertices;
        mesh.RecalculateBounds();
    }
}

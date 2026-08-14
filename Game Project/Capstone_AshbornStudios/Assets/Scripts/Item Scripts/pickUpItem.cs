using UnityEngine;
using System.Collections;
public class pickUpItem : MonoBehaviour
{
    public string itemName;
    public string description;
    public Sprite icon;
    public int count = 1;
    public ItemEnum enu;
    public BlockObject worldPrefab;
    public float collectionTimer = 10;
    bool collected = true;
    public float heightOffGround = 0.5f;
    public float bobHeight = 0.5f;
    public float gravity = 5f;
    public float bobSpeed = 2f;
    //positions to swap between
    float lowPos;
    float highPos;
    //gets player
    GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(startBlock(collectionTimer));
        StartCoroutine(rotate());
        StartCoroutine(dropToGround());
    }
    //rotates it
    IEnumerator rotate()
    {
        while (true)
        {
            Vector3 rotationToAdd = new Vector3(0, .5f, 0);
            transform.Rotate(rotationToAdd);
            yield return new WaitForSeconds(.001f);

        }
    }
    IEnumerator dropToGround()
    {
        while (!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), heightOffGround))
        {
            if(Vector3.Distance(transform.position, player.transform.position) < 1f && !collected)
            {
                collect();
            }
            else if (Vector3.Distance(transform.position, player.transform.position) < 2f && !collected)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 5f * Time.deltaTime);
            }
            yield return null;
        }
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        RaycastHit hit;
        Ray downHit = new Ray(transform.position, Vector3.down);
        Physics.Raycast(downHit, out hit);
        highPos = hit.point.y + heightOffGround + bobHeight / 2.5f;
        lowPos = hit.point.y + heightOffGround - bobHeight / 2.5f;
        StartCoroutine(raise());
    }
    //lowers the block
    IEnumerator lower()
    {
        float time = 0;
        while (time < bobSpeed)
        {

            if (Vector3.Distance(transform.position, player.transform.position) < 1f && !collected)
            {
                collect();
            }
            else if (Vector3.Distance(transform.position, player.transform.position) < 2f && !collected)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 5f * Time.deltaTime);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, Mathf.Lerp(highPos, lowPos, time / bobSpeed), transform.position.z);
                time += Time.deltaTime;
            }
            yield return null;
        }
        StartCoroutine(raise());

    }
    //raises the block
    IEnumerator raise()
    {
        float time = 0;

        while (time < bobSpeed)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < 1f && !collected)
            {
                collect();
            }
            else if (Vector3.Distance(transform.position, player.transform.position) < 2f && !collected)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 5f * Time.deltaTime);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, Mathf.Lerp(lowPos, highPos, time / bobSpeed), transform.position.z);
                time += Time.deltaTime;
            }
            yield return null;
        }
        StartCoroutine(lower());


    }
    private void collect()
    {
        collected = true;
        StartCoroutine(resetCollectionTimer(collectionTimer));
        Item ite = new Item(itemName, description, icon, count, enu, worldPrefab.dropped);
        player.gameObject.GetComponent<PlayerController>().addItemInventory(ite, this);
    }
    public void itemDestroy()
    {
        Destroy(this.gameObject);
    }
    IEnumerator resetCollectionTimer(float amount)
    {
        RaycastHit hit;
        Ray downHit = new Ray(transform.position, Vector3.down);
        Physics.Raycast(downHit, out hit);
        highPos = hit.point.y + heightOffGround + bobHeight / 2.5f;
        lowPos = hit.point.y + heightOffGround - bobHeight / 2.5f;
        yield return new WaitForSeconds(amount);
        collected = false;
    }
    IEnumerator startBlock(float amount)
    {
        yield return new WaitForSeconds(amount);
        collected = false;
    }
}

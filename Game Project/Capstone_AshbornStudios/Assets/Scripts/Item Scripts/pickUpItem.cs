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
    bool collected = false;
    public float bobHeight = 0.5f;
    public float gravity = 5f;
    public float bobSpeed = 2f;
    //positions to swap between
    Vector3 startPos;
    Vector3 lowPos;
    Vector3 highPos;
    //gets player
    GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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
        float time = 0;
        while (!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), bobHeight))
        {

            if (Vector3.Distance(transform.position, player.transform.position) < 2f && !collected)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 5f * Time.deltaTime);
            }
            else
            {
                Vector3 lowering = new Vector3(transform.position.x, transform.position.y - bobHeight, transform.position.z);
                transform.position = Vector3.Lerp(transform.position, lowering, Mathf.Clamp(time / gravity, 0, gravity));
                time += Time.deltaTime;
            }
            yield return null;
        }
        startPos = transform.position;
        lowPos = new Vector3(startPos.x, startPos.y - bobHeight/2.5f, startPos.z);
        highPos = new Vector3(startPos.x, startPos.y + bobHeight/2.5f, startPos.z);
        StartCoroutine(lower());
    }
    //lowers the block
    IEnumerator lower()
    {
        float time = 0;
        while (time < bobSpeed)
        {

            if (Vector3.Distance(transform.position, player.transform.position) < 2f && !collected)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 5f * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.Lerp(highPos, lowPos, time / bobSpeed);
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
            if (Vector3.Distance(transform.position, player.transform.position) < 2f && !collected)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 5f * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.Lerp(lowPos, highPos, time / bobSpeed);
                time += Time.deltaTime;
            }
            yield return null;
        }
        StartCoroutine(lower());


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !collected)
        {
            collected = true;
            StartCoroutine(resetCollectionTimer(collectionTimer));
            Item ite = new Item(itemName, description, icon, count, enu, worldPrefab.dropped);
            other.gameObject.GetComponent<PlayerController>().addItemInventory(ite, this);
        }
    }
    public void itemDestroy()
    {
        Destroy(this.gameObject);
    }
    IEnumerator resetCollectionTimer(float amount)
    {
        lowPos = new Vector3(transform.position.x, startPos.y - .6f, transform.position.z);
        highPos = new Vector3(transform.position.x, startPos.y - .85f, transform.position.z);
        yield return new WaitForSeconds(amount);
        collected = false;
        
    }
}

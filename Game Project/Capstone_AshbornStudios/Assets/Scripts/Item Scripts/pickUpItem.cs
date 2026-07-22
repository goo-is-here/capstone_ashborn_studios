using UnityEngine;
using System.Collections;
public class pickUpItem : MonoBehaviour
{
    public string itemName;
    public string description;
    public Sprite icon;
    public int count = 1;
    public ItemEnum enu;
    public GameObject worldPrefab;
    public float collectionTimer = 10;
    bool collected = false;
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
        startPos = transform.position;
        lowPos = new Vector3(startPos.x, startPos.y - .6f, startPos.z);
        highPos = new Vector3(startPos.x, startPos.y - .85f, startPos.z);
        StartCoroutine(rotate());
        StartCoroutine(lower());
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
    //lowers the block
    IEnumerator lower()
    {
        float time = 0;
        float duration = 2f;
        while (time < duration)
        {

            if (Vector3.Distance(transform.position, player.transform.position) < 2f && !collected)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 5f * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.Lerp(highPos, lowPos, time / duration);
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
        float duration = 2f;

        while (time < duration)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < 2f && !collected)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 5f * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.Lerp(lowPos, highPos, time / duration);
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
            other.gameObject.GetComponent<PlayerController>().addItemInventory(gameObject);
        }
    }
    IEnumerator resetCollectionTimer(float amount)
    {
        lowPos = new Vector3(transform.position.x, startPos.y - .6f, transform.position.z);
        highPos = new Vector3(transform.position.x, startPos.y - .85f, transform.position.z);
        yield return new WaitForSeconds(amount);
        collected = false;
        
    }
}

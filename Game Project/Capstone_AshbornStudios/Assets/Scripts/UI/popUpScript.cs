using UnityEngine;
using TMPro;
using System.Collections;

public class popUpScript : MonoBehaviour
{
    public GameObject text;
    GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        text.transform.rotation.SetLookRotation(-player.transform.position);
        if (Vector3.Distance(player.transform.position, transform.position) > 2)
        {
            text.SetActive(false);
            
        }
        else
        {
            text.SetActive(true);
        }
    }
}

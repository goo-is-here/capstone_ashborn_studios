using UnityEngine;
using TMPro;
using System.Collections;

public class popUpScript : MonoBehaviour
{
    TMP_Text text;
    GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        text = GameObject.FindGameObjectWithTag("Interact").GetComponent<TMP_Text>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) > 5)
        {
            text.enabled = false;
            
        }
        else
        {
            text.enabled = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                print("works");
            }
        }
    }
    IEnumerator flash()
    {
        text.outlineWidth = 1;
        yield return new WaitForSeconds(1f);
        text.outlineWidth = 0;
    }
}

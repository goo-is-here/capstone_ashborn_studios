using UnityEngine;
using TMPro;

public class popUpScript : MonoBehaviour
{
    TMP_Text text;
    GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = this.GetComponent<TMP_Text>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - player.transform.position);
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
}

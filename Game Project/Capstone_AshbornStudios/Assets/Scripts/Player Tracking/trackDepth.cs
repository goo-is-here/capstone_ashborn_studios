using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class trackDepth : MonoBehaviour
{
    TextMeshProUGUI depth;
    GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        depth = GameObject.FindGameObjectWithTag("DepthTracker").GetComponent<TextMeshProUGUI>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(depth != null)
        {
            int dist = (int)player.transform.position.y - (int)transform.position.y;
            depth.text = dist + " meters";
        }
    }
}

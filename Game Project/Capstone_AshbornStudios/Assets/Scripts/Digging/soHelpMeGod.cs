using UnityEngine;

public class soHelpMeGod : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<diggableBlock>().enabled = true;
    }
}

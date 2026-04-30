using UnityEngine;

public class unlockedRoom : MonoBehaviour
{
    public growRoomEnter enter;
    
    // Update is called once per frame
    void Update()
    {
        if(enter.entered == true)
        {
            this.GetComponent<diggableBlock>().hitBlock(9999, transform.position);
            this.enabled = false;
        }
    }
}

using UnityEngine;

public class dirt : MonoBehaviour
{
    [HideInInspector] public int x;
    [HideInInspector] public int y;
    [HideInInspector] public int z;
    [HideInInspector] public bool open = true;
    public void setThis(int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.open = true;
    }
    public void breakObject()
    {
        transform.parent.gameObject.GetComponent<DirtGeneration>().receiveBreak(x, y, z);
        Destroy(this.gameObject);
    }
}

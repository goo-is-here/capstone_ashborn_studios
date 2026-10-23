using UnityEngine;

public class dirt : MonoBehaviour
{
    [HideInInspector] public int x;
    [HideInInspector] public int y;
    [HideInInspector] public int z;
    public void setThis(int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
    public void breakObject()
    {
        transform.parent.gameObject.GetComponent<DirtGeneration>().receiveBreak(x, y, z);
        Destroy(this.gameObject);
    }
}

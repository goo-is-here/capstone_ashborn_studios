using UnityEngine;

public class despawn : MonoBehaviour
{
    float startTime = Time.time;
    public float despawnTime = 1f;
    float destoryThis = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        destoryThis = startTime + despawnTime;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}

using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    //used in a separate script for cutscene camera movements
    Transform headLocation;
    [Header("Camera Follow Speed")]
    public float moveSpeed;
    void Start()
    {
        //gets the location to follow
        headLocation = GameObject.FindGameObjectWithTag("Head").transform;
    }
    //using late update to have physics completed first
    void LateUpdate()
    {
        //lerps to head position
        transform.position = Vector3.Lerp(transform.position, headLocation.position, moveSpeed);
    }

}

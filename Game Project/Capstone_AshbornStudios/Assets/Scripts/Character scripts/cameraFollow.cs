using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    Transform headLocation;
    private float startTime;
    public float moveSpeed;
    float vertCam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        headLocation = GameObject.FindGameObjectWithTag("Head").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, headLocation.position, moveSpeed);
    }

}

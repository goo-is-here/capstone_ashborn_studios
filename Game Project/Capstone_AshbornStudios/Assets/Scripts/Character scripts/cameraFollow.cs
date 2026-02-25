using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    Transform headLocation;
    private float startTime;
    public float moveSpeed;

    private float yaw = 0.0f;
    private float pitch = 0.0f;
    float vertCam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        headLocation = GameObject.FindGameObjectWithTag("Head").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float inputY = Input.GetAxis("Mouse Y") * 5f;
        transform.position = Vector3.Lerp(transform.position, headLocation.position, moveSpeed);
        vertCam -= inputY;
        vertCam = Mathf.Clamp(vertCam, -90f, 90f);
        transform.localEulerAngles = Vector3.right * vertCam;

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, headLocation.parent.transform.eulerAngles.y, 0.0f);
    }
}

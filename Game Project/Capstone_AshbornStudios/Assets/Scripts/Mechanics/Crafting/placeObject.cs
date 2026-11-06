using UnityEngine;
using System.Collections;
public class placeObject : MonoBehaviour
{
    [SerializeField] characterInventory invent;
    public GameObject furniture;
    [SerializeField] float refreshRate;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float rotateAmount;
    bool refreshed = false;
    public bool placeItem;
    int rotate = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(moveObject());
    }

    // Update is called once per frame
    void Update()
    {
        if(invent.selectedSlotNum >= 0 && invent.selectedSlotNum < invent.hotBarSlots && invent.furnitureToDestroy != null)
        {
            if (!refreshed && furniture != null && !furniture.GetComponent<collisionChecker>().placed && placeItem)
            {
                StartCoroutine(moveObject());
            }
            if (Input.GetKeyDown(KeyCode.R) && !furniture.GetComponent<collisionChecker>().placed)
            {
                Vector3 rotationToObject = new Vector3(0, 0, rotateAmount);
                furniture.transform.Rotate(rotationToObject);
                rotate++;
                if(rotate > 3)
                {
                    rotate = 0;
                }
            }
        }
    }
    IEnumerator moveObject()
    {
        refreshed = true;
        RaycastHit hit;
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, groundMask)){
            furniture.transform.position = hit.point;
        }
        yield return new WaitForSeconds(refreshRate);
        refreshed = false;
    }
}

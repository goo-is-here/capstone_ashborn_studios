using UnityEngine;
using System.Collections;

public class changeMat : MonoBehaviour
{
    [SerializeField] Color hitMat;
    [SerializeField] float refreshRate;
    PlayerController cont;
    bool refreshed = true;
    private void Start()
    {
        cont = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    private void Update()
    {
        if (refreshed)
        {

        }
    }
    IEnumerator moveDigSphere()
    {
        refreshed = false;
        RaycastHit hit;
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward), out hit, cont.diggingRange))
        {
            gameObject.transform.position = hit.point;
        }
        else
        {
            gameObject.transform.position = hit.point;
        }
        yield return new WaitForSeconds(refreshRate);
        refreshed = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dirt"))
        {
            other.GetComponent<MeshRenderer>().material.color = hitMat;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Dirt"))
        {
            other.GetComponent<MeshRenderer>().material.color = Color.white;
        }
    }
}

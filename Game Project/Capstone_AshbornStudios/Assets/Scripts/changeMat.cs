using UnityEngine;
using System.Collections;

public class changeMat : MonoBehaviour
{
    [SerializeField] Color hitMat;
    [SerializeField] float refreshRate;
    PlayerController cont;
    bool refreshed = true;
    [SerializeField] LayerMask digLayer;
    private void Start()
    {
        cont = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    private void Update()
    {
        if (refreshed)
        {
            StartCoroutine(moveDigSphere());
        }
        gameObject.GetComponent<SphereCollider>().radius = cont.diggingRange * 0.9f;
    }
    IEnumerator moveDigSphere()
    {
        refreshed = false;
        RaycastHit hit;
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward), out hit, cont.diggingRange, digLayer))
        {
            gameObject.transform.position = hit.point;
        }
        else
        {
            Vector3 position = Camera.main.transform.position + Camera.main.transform.TransformDirection(Vector3.forward) * cont.diggingRange;
            gameObject.transform.position = position;
        }
        yield return new WaitForSeconds(refreshRate);
        refreshed = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        print(other.tag);
        print(other.name);
        if (other.CompareTag("Dirt"))
        {
            print("this");
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

using UnityEngine;

public class changeMat : MonoBehaviour
{
    [SerializeField] Material hitMat;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dirt"))
        {
            other.GetComponent<MeshRenderer>().material = hitMat;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Dirt"))
        {
            other.GetComponent<MeshRenderer>().material = other.GetComponent<dirt>().standMat;
        }
    }
}

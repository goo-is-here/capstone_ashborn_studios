using Unity.VisualScripting;
using UnityEngine;

public class Item_Pickup : MonoBehaviour
{
    [Header("Assign in Inspector")]
    public GameObject item;
    public int amount = 1;

    private void Reset()
    {
        
        Collider col = GetComponent<Collider>();
        if (col != null) col.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {


        if (other.CompareTag("Player") && item != null)
        {

            Destroy(gameObject);
        }

       

        
       
    }


}
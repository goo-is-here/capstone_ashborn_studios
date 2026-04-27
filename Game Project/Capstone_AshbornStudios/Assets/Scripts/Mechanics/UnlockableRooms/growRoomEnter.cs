using UnityEngine;

public class growRoomEnter : MonoBehaviour
{
    public bool entered = false;
    GameObject[] plants;
    private void Start()
    {
        plants = GameObject.FindGameObjectsWithTag("plant");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !entered)
        {
            entered = true;
            for(int i = 0; i < plants.Length; i++)
            {
                plants[i].GetComponent<growPlant>().resetScale();
            }
        }
    }
}

using UnityEngine;

public class climbingTest : MonoBehaviour
{
    RaycastHit feetClimb;
    public bool climbing = false;
    public float lengthOfClimb;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    private void Update()
    {
       if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out feetClimb, 1f)){
            climbing = true;
       }
        else
        {
            climbing = false;
        }
    }
    // Update is called once per frame
    
}

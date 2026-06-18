using UnityEngine;

public class climbingTest : MonoBehaviour
{
    RaycastHit feetClimb;
    [HideInInspector]
    public bool climbing = false;
    [Header("Distance to Start Climb")]
    public float lengthOfClimb;
    [Header("LayerMask to Ignore")]
    public LayerMask SafeZone;
    
    //this method checks if there is some object in front of the player
    private void Update()
    {
        //safe zone layer should be used if it is to be ignored, sets climbing bool to be used by movement
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out feetClimb, 1f, ~SafeZone)){
            climbing = true;
        }
        else
        {
            climbing = false;
        }
    }
    
}

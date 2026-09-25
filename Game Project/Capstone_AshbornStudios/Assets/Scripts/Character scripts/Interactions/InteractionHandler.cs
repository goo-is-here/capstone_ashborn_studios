using Unity.VisualScripting;
using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    [Header("Parameters")]
    public float interactDistance = 3.0f;
    public Transform cam;
    public LayerMask interactableLayermask;
    public bool canInteract = true;

    //Tries to interact with object
    public void Interact()
    {
        print("Try Interact");
        Ray ray = new Ray(cam.position, cam.forward);
        if(Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactableLayermask))
        {
            if(hit.collider.TryGetComponent(out IInteractable interactable))
            {
                if (interactable.CanInteract(this))
                {
                    interactable.Interact(this);
                }
            }
        }
    }
}
using UnityEngine;

public interface IInteractable
{
    bool CanInteract(InteractionHandler interactor);
    void Interact(InteractionHandler interactor);
}

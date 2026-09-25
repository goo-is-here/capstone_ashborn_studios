using UnityEngine;

public class PassiveHolder : MonoBehaviour, IInteractable
{
    public GemPassive passive;

    public bool CanInteract(InteractionHandler interactor) => true;

    public void Interact(InteractionHandler interactor)
    {
        interactor.gameObject.GetComponent<PassivesManager>().EquipPassive(passive);
        print("InteractedWith");
    }

}

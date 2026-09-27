using UnityEngine;

public class PassiveHolder : MonoBehaviour, IInteractable, IDataPersistence
{
    public GemPassive passive;

    public Material activeMaterial;
    public Material inactiveMaterial;


    public bool CanInteract(InteractionHandler interactor) => true;


    public void Interact(InteractionHandler interactor)
    {
        var passiveManager = interactor.gameObject.GetComponent<PassivesManager>();
        passiveManager.EquipPassive(passive);
        print("Interacted With");
        if (passiveManager.equippedPassives.Contains(passive))
        {
            SetInactiveMat();
        }
        else
        {
            SetActiveMat();
        }
    }

    public void SaveData(ref GameData data)
    {
        return;
    }

    public void LoadData(GameData data)
    {
        if (data.equippedPassives.Contains(passive))
        {
            SetInactiveMat();
        }
    }

    private void SetInactiveMat()
    {
        GetComponent<Renderer>().material = inactiveMaterial;
    }

    private void SetActiveMat()
    {
        GetComponent<Renderer>().material = activeMaterial;
    }

}

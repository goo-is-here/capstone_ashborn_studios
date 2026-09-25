using UnityEngine;

[CreateAssetMenu(fileName = "GemPassive", menuName = "Scriptable Objects/GemPassive")]
public abstract class GemPassive : ScriptableObject
{
    protected PlayerController player;
    

    public virtual void Initialize(PlayerController playerController)
    {
        player = playerController;
    }

    public virtual void Unequip()
    {
        player = null;
    }
}

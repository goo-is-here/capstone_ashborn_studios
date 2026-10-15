using UnityEngine;

[CreateAssetMenu(fileName = "StackSizeIncreaseEffect", menuName = "Scriptable Objects/GemPassive/StackSizeIncreaseEffect")]
public class StackSizeIncreaseEffect : GemPassive
{
    public int stackSizeIncreaseAmount = 2;

    public override void Initialize(PlayerController playerController)
    {
        base.Initialize(playerController);
        player.inventory.GetComponent<characterInventory>().increaseStackSizeAmount(stackSizeIncreaseAmount);
    }

    public override void Unequip()
    {
        player.inventory.GetComponent<characterInventory>().increaseStackSizeAmount(-stackSizeIncreaseAmount);
        base.Unequip();
    }
}

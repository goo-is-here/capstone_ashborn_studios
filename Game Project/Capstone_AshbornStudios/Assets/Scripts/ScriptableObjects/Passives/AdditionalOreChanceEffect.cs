using UnityEngine;

[CreateAssetMenu(fileName = "AdditionalOreChanceEffect", menuName = "Scriptable Objects/GemPassive/AdditionalOreChanceEffect")]
public class AdditionalOreChanceEffect : GemPassive
{
    public int oreChancePercentageIncrease = 5;

    public override void Initialize(PlayerController playerController)
    {
        base.Initialize(playerController);
        player.additionalOreChance += oreChancePercentageIncrease;
    }

    public override void Unequip()
    {
        player.additionalOreChance -= oreChancePercentageIncrease;
        base.Unequip();
    }
}

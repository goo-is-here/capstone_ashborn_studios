using UnityEngine;

[CreateAssetMenu(fileName = "LanternRangeIncreaseEffect", menuName = "Scriptable Objects/GemPassive/LanternRangeIncreaseEffect")]
public class LanternRangeIncreaseEffect : GemPassive
{
    public float rangeIncrease = 0.2f;

    public override void Initialize(PlayerController playerController)
    {
        base.Initialize(playerController);
        player.GetComponentInChildren<Lantern>().detectionDistanceModifier += rangeIncrease;
    }

    public override void Unequip()
    {
        player.GetComponentInChildren<Lantern>().detectionDistanceModifier -= rangeIncrease;
        base.Unequip();
    }
}

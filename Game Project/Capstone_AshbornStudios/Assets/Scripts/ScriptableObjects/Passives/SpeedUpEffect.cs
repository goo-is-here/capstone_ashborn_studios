using UnityEngine;

[CreateAssetMenu(fileName = "SpeedUpEffect", menuName = "Scriptable Objects/GemPassive/SpeedUpEffect")]
public class SpeedUpEffect : GemPassive
{
    public float moveSpeedMultiplier = 2f;

    public override void Initialize(PlayerController playerController)
    {
        base.Initialize(playerController);
        player.movementSpeed *= moveSpeedMultiplier;
    }

    public override void Unequip()
    {
        player.movementSpeed /= moveSpeedMultiplier;
        base.Unequip();
    }
}

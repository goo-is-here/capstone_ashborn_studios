using UnityEngine;
using TMPro;

public class ToolTier_UI : MonoBehaviour
{
    public PlayerController player;
    public TMP_Text toolTierText;

    private void Update()
    {
        if (player == null || toolTierText == null) return;

        toolTierText.text = "Tool: " + player.GetToolTierName() +
                     " (Tier " + player.GetToolTierLevel() + ")" +
                     "\nDamage: " + player.GetToolTierValue();
    }
}
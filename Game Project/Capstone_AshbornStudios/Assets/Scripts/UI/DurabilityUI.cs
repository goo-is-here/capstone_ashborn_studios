using UnityEngine;
using TMPro;

public class DurabilityUI : MonoBehaviour
{
    public PlayerController player;
    public TMP_Text usesText;

    void Update()
    {
        if (player == null || usesText == null) return;

        usesText.text = "Uses: " +
                        player.GetUsesLeft() +
                        " / " +
                        player.GetMaxUses();
    }
}
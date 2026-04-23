using UnityEngine;
using TMPro;

public class Depth_UI : MonoBehaviour
{
    public Depth_Tracker depthTracker;
    public TMP_Text depthText;

    private void Update()
    {
        if (depthTracker == null || depthText == null) return;

        depthText.text = "Depth: " + Mathf.RoundToInt(depthTracker.currentDepth).ToString();
    }
}
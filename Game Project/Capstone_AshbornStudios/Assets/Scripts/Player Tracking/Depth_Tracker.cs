using UnityEngine;

public class Depth_Tracker : MonoBehaviour
{
    public Transform player;

    [Header("Depth Settings")]
    public float surfaceY = 0f;
    public float currentDepth;

    private void Update()
    {
        if (player == null) return;

        float playerY = player.position.y;

      
        currentDepth = surfaceY - playerY;

        
        if (Mathf.Abs(currentDepth) < 0.01f)
            currentDepth = 0f;

        
        currentDepth = Mathf.Round(currentDepth * 100f) / 100f;
    }
}

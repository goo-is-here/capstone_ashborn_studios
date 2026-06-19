using UnityEngine;

public class Depth_Tracker : MonoBehaviour
{
    public Transform player;

    [Header("Depth Settings")]
    public float surfaceY = 128f;
    public float currentDepth;
    //gets player
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        //makes sure player isn't null and gets y
        if (player == null) return;

        float playerY = player.position.y;

        //finds difference
        currentDepth = surfaceY - playerY;

        //TODO Replace
        if (Mathf.Abs(currentDepth) < 0.01f)
            currentDepth = 0f;

        
        currentDepth = Mathf.Round(currentDepth * 100f) / 100f;
    }
}

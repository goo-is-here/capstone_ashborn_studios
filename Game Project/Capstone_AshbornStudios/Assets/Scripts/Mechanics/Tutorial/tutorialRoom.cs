using UnityEngine;

public class tutorialRoom : MonoBehaviour
{
    public DirtGeneration tutorialTunnel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tutorialTunnel.generateDirt(.45f, 0);
    }
}

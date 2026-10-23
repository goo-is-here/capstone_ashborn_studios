using UnityEngine;

public class moveScreen : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Screen.fullScreenMode = FullScreenMode.Windowed;
        print(Screen.mainWindowDisplayInfo);
        Screen.MoveMainWindowTo(Screen.mainWindowDisplayInfo, Screen.mainWindowPosition);
        Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
    }

    
}

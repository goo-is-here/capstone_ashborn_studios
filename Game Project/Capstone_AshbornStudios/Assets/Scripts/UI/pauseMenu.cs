using UnityEngine;
using UnityEngine.UI;

public class pauseMenu : MonoBehaviour
{
    PlayerController cont;
    Image pause;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cont = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        pause = GameObject.FindGameObjectWithTag("Pause").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            cont.canMove = !cont.canMove;
            pause.enabled = !pause.enabled;
            if(cont.canMove)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
}

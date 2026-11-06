using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GemNotification : MonoBehaviour
{
    //Refrences
    public PlayerController player;
    public Image gemIcon;
    public TextMeshProUGUI gemText;
    public Jounal journal;

    //private variables
    private bool isActive;

    private void Update()
    {
        if (isActive)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.J))
            {
                GetComponent<Canvas>().enabled = false;
            }
        }
    }

    public void SetupNotification(Sprite newIcon, string name, int gemID)
    {
        gemIcon.sprite = newIcon;
        gemText.text = name;
        GetComponent<Canvas>().enabled = true;
        journal.SetPage(gemID / 2);
    }

}

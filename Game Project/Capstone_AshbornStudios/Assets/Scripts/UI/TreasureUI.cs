using System.Collections.Generic;
using UnityEngine;

public class TreasureUI : MonoBehaviour
{
    public GameObject treasureRoom;
    private bool isOpen = false;

    private void Start()
    {
        if (treasureRoom != null)
            treasureRoom.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            ToggleTreasureUI();
        }
    }

    public void ToggleTreasureUI()
    {
        if (treasureRoom == null)
        {
            Debug.LogWarning("Treasure panel is not assigned.");
            return;
        }

        isOpen = !isOpen;
        treasureRoom.SetActive(isOpen);

        Cursor.lockState = isOpen ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isOpen;
    }
}

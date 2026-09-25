using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PassivesManager : MonoBehaviour
{
    [Header("Passives")]
    public List<GemPassive> equippedPassives = new List<GemPassive>();

    public float maxPassives = 1;

    private PlayerController player;

    private void Start()
    {
        player = GetComponent<PlayerController>();
    }


    public void EquipPassive(GemPassive newPassive)
    {
        if(!(equippedPassives.Count >= maxPassives))
        {
            equippedPassives.Add(newPassive);
            newPassive.Initialize(player);
        }
    }

    public void UnequipPassive(GemPassive passiveToRemove)
    {
        if (equippedPassives.Contains(passiveToRemove))
        {
            equippedPassives.Remove(passiveToRemove);
            passiveToRemove.Unequip();
        }


    }

}

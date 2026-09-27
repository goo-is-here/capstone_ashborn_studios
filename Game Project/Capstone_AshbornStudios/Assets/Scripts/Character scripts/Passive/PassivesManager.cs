using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PassivesManager : MonoBehaviour, IDataPersistence
{
    //refrences and settigns
    [Header("Passives")]
    public List<GemPassive> equippedPassives = new List<GemPassive>();

    public float maxPassives = 1;

    private PlayerController player;

    //gets the player controller
    private void Start()
    {
        player = GetComponent<PlayerController>();
    }

    //handles checking wether or not a passive is already equipped and weather or not to equip it
    public void EquipPassive(GemPassive newPassive)
    {
        if (equippedPassives.Contains(newPassive))
            UnequipPassive(newPassive);

        else if (!(equippedPassives.Count >= maxPassives))
        {
            equippedPassives.Add(newPassive);
            newPassive.Initialize(player);
        }
    }

    //handles unequipping a passive
    public void UnequipPassive(GemPassive passiveToRemove)
    {
        print("Unequip");
        equippedPassives.Remove(passiveToRemove);
        passiveToRemove.Unequip();
    }

    //save and load
    public void SaveData(ref GameData data)
    {
        data.equippedPassives = equippedPassives;
    }

    public void LoadData(GameData data)
    {
        equippedPassives = data.equippedPassives;
        foreach(GemPassive passive in equippedPassives)
        {
            passive.Initialize(player);
        }
    }
}

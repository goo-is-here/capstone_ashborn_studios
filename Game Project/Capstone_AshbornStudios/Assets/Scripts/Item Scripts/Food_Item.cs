using UnityEngine;

public class Food_Item : Item
{
    //TODO redo
    //amount of hunger to replace
    public float hungerRestoreAmount;

    //constructor for food item
    public Food_Item(string name, string description, Sprite icon, int count, ItemEnum enu, GameObject worldPrefab, float hungerRestoreAmount)
        : base(name, description, icon, count, enu, worldPrefab)
    {
        this.hungerRestoreAmount = hungerRestoreAmount;
    }
}

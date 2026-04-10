using UnityEngine;

public class Food_Item : Item
{
    public float hungerRestoreAmount;

    public Food_Item(string name, string description, Sprite icon, int count, GameObject worldPrefab, float hungerRestoreAmount)
        : base(name, description, icon, count, worldPrefab)
    {
        this.hungerRestoreAmount = hungerRestoreAmount;
    }
}

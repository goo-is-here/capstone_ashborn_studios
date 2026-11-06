using UnityEngine;

[CreateAssetMenu(fileName = "FurnitureObject", menuName = "Scriptable Objects/FurnitureObject")]
public class FurnitureObject : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public ItemEnum enu;
    public GameObject worldPrefab;
}

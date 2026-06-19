using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "Scriptable Objects/Recipe")]
public class Recipe : ScriptableObject
{
    //recipe vairables
    public Node[] recipe;
    public string madeName;
    public string description;
    public Sprite icon;
    public int makeCount;
    public GameObject worldPrefab;
    public ItemEnum itemEnum;
    
}
//public variables to change in inspector
[System.Serializable]
public class Node
{
    public ItemEnum enu;
    public string Name;
    public int count;
}
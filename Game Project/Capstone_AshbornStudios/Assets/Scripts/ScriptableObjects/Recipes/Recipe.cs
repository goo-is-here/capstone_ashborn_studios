using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "Scriptable Objects/Recipe")]
public class Recipe : ScriptableObject
{
    public Node[] recipe;
    string madeName;
    string description;
    public Sprite icon;
    public int makeCount;
    public GameObject worldPrefab;
    public ItemEnum itemEnum;
    
}
[System.Serializable]
public class Node
{
    public ItemEnum enu;
    public int count;
}
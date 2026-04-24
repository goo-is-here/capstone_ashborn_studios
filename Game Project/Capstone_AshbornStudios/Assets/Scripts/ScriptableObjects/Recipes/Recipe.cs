using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "Scriptable Objects/Recipe")]
public class Recipe : ScriptableObject
{
    public Node[] recipe;
    public string madeName;
    public string description;
    public Sprite icon;
    public int makeCount;
    public GameObject worldPrefab;
    public ItemEnum itemEnum;
    
}
[System.Serializable]
public class Node
{
    public ItemEnum enu;
    public string Name;
    public int count;
}
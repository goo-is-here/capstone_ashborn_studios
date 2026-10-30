using UnityEngine;

[CreateAssetMenu(fileName = "ClutterObject", menuName = "Scriptable Objects/ClutterObject")]
public class ClutterObject : ScriptableObject
{
    public int width;
    public int length;
    public int height;
    public GameObject obj;
}

using UnityEngine;

[CreateAssetMenu(fileName = "BlockObject", menuName = "Scriptable Objects/BlockObject")]
public class BlockObject : ScriptableObject
{
    public Material mat;
    public GameObject particles;
    public GameObject dropped;
    public float blockHealth;
    public float minDamage;
    public blockType type;
    public AudioClip breaking;
    public AudioClip broke;
}

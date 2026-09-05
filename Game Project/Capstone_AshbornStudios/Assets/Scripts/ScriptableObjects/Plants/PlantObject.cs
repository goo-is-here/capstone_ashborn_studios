using UnityEngine;

[CreateAssetMenu(fileName = "PlantObject", menuName = "Scriptable Objects/PlantObject")]
public class PlantObject : ScriptableObject
{
    //mesh for each stage of growth
    public Mesh meshStage1;
    public Mesh meshStage2;
    public Mesh meshStage3;
    public Mesh meshStage4;

    float timeToGrow;
    bool repeatedlyHarvestable; //for whether or not the plant needs to be replanted or if it can just be harvested over and over again
    //figure out type of object for finaldrop
}

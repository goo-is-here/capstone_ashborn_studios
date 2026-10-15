using UnityEngine;
using System.Collections;
[System.Serializable]
public class chunkData
{
    public int chunkPositionx;
    public int chunkPositiony;
    public int chunkPositionz;
    public bool[] chunkBreakData;
    public chunkData(bool[] breakData, int x, int y, int z)
    {
        this.chunkPositionx = x;
        this.chunkPositiony = y;
        this.chunkPositionz = z;
        this.chunkBreakData = (bool[])breakData.Clone();
    }
}

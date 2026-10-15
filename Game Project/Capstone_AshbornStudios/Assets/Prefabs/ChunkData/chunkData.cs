using UnityEngine;
using System.Collections;
[System.Serializable]
public class chunkData
{
    public int chunkPositionx;
    public int chunkPositiony;
    public int chunkPositionz;
    public int chunkBreakData;
    public chunkData(int breakData, int x, int y, int z)
    {
        this.chunkPositionx = x;
        this.chunkPositiony = y;
        this.chunkPositionz = z;
        this.chunkBreakData = breakData;
    }
}

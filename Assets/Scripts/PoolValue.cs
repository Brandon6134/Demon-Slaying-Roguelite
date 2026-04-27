using System;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class PoolValue 
{
    public Pool pool {get; private set;}
    public float value {get; private set;}
    public int[] spawnNums {get; private set;}

    public PoolValue(Pool pool, float value, int spawnNumSize)
    {
        this.pool = pool;
        this.value = value;
        this.spawnNums = new int[spawnNumSize];
    }

    public void AddSpawnNum(int index, int num)
    {
        spawnNums[index] += num;
    }

    public void ResetSpawnNums()
    {
        Array.Clear(spawnNums,0,spawnNums.Length);
    }
}

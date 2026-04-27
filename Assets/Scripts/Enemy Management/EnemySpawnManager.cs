using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public static EnemySpawnManager Instance;
    [SerializeField] Pool[] enemyPools;

    void Start()
    {
        Instance = this;

    }

    public void SpawnMiniWave(List<PoolValue> poolValues,int miniWaveNum)
    {
        foreach(PoolValue pV in poolValues)
        {
            int numToSpawn = pV.spawnNums[miniWaveNum];

            for (int i = 0; i < numToSpawn; i++)
            {
                Vector3 spawnPos = new Vector3(UnityEngine.Random.Range(-9f,9f),UnityEngine.Random.Range(-4f,4f),0);
                pV.pool.Spawn(spawnPos);
            }
        }
    }

    public Pool GetEnemyPool(int poolIndex)
    {
        return enemyPools[poolIndex];
    }
}

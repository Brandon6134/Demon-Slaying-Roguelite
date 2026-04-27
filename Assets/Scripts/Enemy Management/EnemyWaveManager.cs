using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyWaveManager : MonoBehaviour
{
    public static EnemyWaveManager Instance;
    private EnemySpawnManager enemySpawnManager;
    private int waveNumber = 1;
    public float requiredKills;
    private float currentKills;
    public int waveCompletionKillIncreaseBy;
    private int miniWavesNum = 3;
    private List<PoolValue> currentEnemyPoolValues = new List<PoolValue>(); //active list of PoolValues of demon types that can be chosen from to spawn in waves
    private int miniWaveIndexCounter = 0; //tracks if on miniwave index # 0,1,2,...etc.
    float[] miniWaveCount; //contains the total num of enemies per miniwave, e.g. [2,2,3] 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
        enemySpawnManager = EnemySpawnManager.Instance;
        miniWaveCount = new float[miniWavesNum]; //set length to num of mini waves

        currentEnemyPoolValues.Add(InitializeEnemyPoolValue(0)); //add basic demon to PoolValue
        CalculateMiniWaves(currentEnemyPoolValues); //calc mini wave on game start
        
    }

    void CheckKills()
    {
        if (requiredKills <= currentKills)
        {
            waveNumber++;
            currentKills = 0;
            requiredKills += waveCompletionKillIncreaseBy;
            HUDManager.Instance.UpdateWaveNumber();
            HUDManager.Instance.UpdateObjectiveText();

            CheckIfAddEnemyToPoolValues(currentEnemyPoolValues); //check if new enemy is to be added to enemy pool value list
            CalculateMiniWaves(currentEnemyPoolValues); //calc and start new mini wave 
        }

        else if (miniWaveCount[miniWaveIndexCounter] <= currentKills) //if mini wave is completed
        {
            miniWaveIndexCounter++;
            enemySpawnManager.SpawnMiniWave(currentEnemyPoolValues,miniWaveIndexCounter);
        }
    }

    public void AddKill(float killValue)
    {
        currentKills += killValue;
        HUDManager.Instance.UpdateObjectiveText();
        CheckKills();
    }

    void CalculateMiniWaves(List<PoolValue> poolValues)
    {
        miniWaveIndexCounter = 0; //reset global miniwave index counter
        ResetAllPoolValues(poolValues); // reset all pool value spawn numbers;

        float miniWaveEnemyNum = requiredKills / miniWavesNum; // calculate kill points per wave
        float miniWaveRemainder = requiredKills % miniWavesNum; // handle remainder division

        //set miniwavecount to e.g. [2,4,7], if the required kills is 7, and there are 3 miniwaves
        for (int i = 0; i < miniWaveCount.Length; i++)
            miniWaveCount[i] = miniWaveEnemyNum * (i+1);
        miniWaveCount[^1] += miniWaveRemainder;

        int calcMiniWaveIndexCounter = 0; 

        float[] miniWavesToSpawn = new float[miniWavesNum]; //holds num of kills to be spawned each wave, e.g. if miniwavecount = [2,4,7], then this is [2,2,3]

        for (int i = 0; i < miniWaveCount.Length; i++) //set value
        {
            if (i==0)
                miniWavesToSpawn[i] = miniWaveCount[i];
            else
                miniWavesToSpawn[i] = miniWaveCount[i] - miniWaveCount[i-1];
        }

        foreach (float count in miniWavesToSpawn)
        {
            float num = count;
            while(num > 0f) //while kill num is above 0, keep adding enemies to mini wave
            {
                float value = 10000f;
                int poolIndex = -1;
                while (num - value < 0f) //if value is below 0, repeat the random generation (e.g. if wave value of 1 is left, demon value of 2 is chosen, but 1 - 2 < 0. thus loop.)
                {
                    poolIndex = UnityEngine.Random.Range(0,poolValues.Count); //choose random demon pool index
                    value = poolValues[poolIndex].value; //get demon kill value
                }
                
                num -= value; //subtract that demon's kill value from current mini wave kill num
                poolValues[poolIndex].AddSpawnNum(calcMiniWaveIndexCounter,1); //add one to that enemy's spawn count for that index mini wave
            }
            calcMiniWaveIndexCounter++; //go to next mini wave calc
        }

        Debug.Log(string.Join(" | ",
            poolValues.Select(p =>
        $"{p.pool.name}: [{string.Join(", ", p.spawnNums)}]"
            )
        ));

        enemySpawnManager.SpawnMiniWave(currentEnemyPoolValues,miniWaveIndexCounter); //call first mini wave
    }

    PoolValue InitializeEnemyPoolValue(int enemyPoolIndex)
    {
        Pool pool = enemySpawnManager.GetEnemyPool(enemyPoolIndex);
        return new PoolValue(pool,pool.prefab.GetComponent<DemonBehaviour>().waveValue,miniWavesNum);
    }

    void ResetAllPoolValues(List<PoolValue> poolValues)
    {
        foreach (PoolValue pV in poolValues)
        {
            pV.ResetSpawnNums();
        }
    }

    void CheckIfAddEnemyToPoolValues(List<PoolValue> poolValueList)
    {
        if (waveNumber==3)
            poolValueList.Add(InitializeEnemyPoolValue(1));
        if (waveNumber==5)
            poolValueList.Add(InitializeEnemyPoolValue(2));
    } 

    public string GetWaveNumber()
    {
        return waveNumber.ToString();
    }

    public string GetCurrentKills()
    {
        return currentKills.ToString();
    }

    public string GetRequiredKills()
    {
        return requiredKills.ToString();
    }
}

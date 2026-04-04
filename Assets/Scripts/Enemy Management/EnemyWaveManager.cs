using System;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{
    public static EnemyWaveManager Instance;
    private int waveNumber = 1;
    public int requiredKills;
    private int currentKills;
    public int waveCompletionKillIncreaseBy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
    }

    void CheckKills()
    {
        if (requiredKills <= currentKills)
        {
            waveNumber++;
            currentKills = 0;
            requiredKills += waveCompletionKillIncreaseBy;
            EnemySpawnManager.Instance.DecreaseSpawnInterval(0.5f);
            HUDManager.Instance.UpdateWaveNumber();
            HUDManager.Instance.UpdateObjectiveText();
        }
    }

    public void AddKill()
    {
        currentKills++;
        HUDManager.Instance.UpdateObjectiveText();
        CheckKills();
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

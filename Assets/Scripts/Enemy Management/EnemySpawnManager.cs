using System.Collections;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public static EnemySpawnManager Instance;
    [SerializeField] Pool enemyPool;
    private bool isSpawning = true;
    private float spawnInterval = 3f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
        StartCoroutine(SpawnInIntervals());
    }

    IEnumerator SpawnInIntervals()
    {
        while(isSpawning)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-9f,9f),Random.Range(-4f,4f),0);
            enemyPool.Spawn(spawnPos);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void DecreaseSpawnInterval(float decreaseIntervalAmount)
    {
        spawnInterval -= decreaseIntervalAmount;
        if (spawnInterval < 0.5f) //cap off at 0.5 reset
            spawnInterval = 0.5f;
    }
}

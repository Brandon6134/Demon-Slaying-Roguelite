using System.Collections;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] Pool enemyPool;
    private bool isSpawning = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnInIntervals());
    }

    IEnumerator SpawnInIntervals()
    {
        while(isSpawning)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-9f,9f),Random.Range(-4f,4f),0);
            enemyPool.Spawn(spawnPos);
            yield return new WaitForSeconds(3f);
        }
        
    }
}

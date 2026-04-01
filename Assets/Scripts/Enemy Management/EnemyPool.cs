using UnityEngine;
using System.Collections.Generic;

public class EnemyPool : MonoBehaviour
{
    private Queue<GameObject> pool = new Queue<GameObject>();
    public GameObject enemyPrefab;
    public int poolSize = 20;

    void Awake()
    {
        for(int i=0;i<poolSize;i++) //initialize pool with preset number of inactive enemies
        {
            GameObject enemy = Instantiate(enemyPrefab,this.transform); //instantiate enemies with this gameobject as the parent
            enemy.SetActive(false);
            pool.Enqueue(enemy);
        }
    }

    public void SpawnEnemy(Vector3 spawnPos)
    {
        GameObject enemy = pool.Dequeue();
        enemy.SetActive(true);
        enemy.transform.position = spawnPos;
    }

    public void DespawnEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
        pool.Enqueue(enemy);
        //more reset stuff here like health, etc...
    }

}

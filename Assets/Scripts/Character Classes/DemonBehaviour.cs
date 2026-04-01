using UnityEngine;
using UnityEngine.UIElements;

public class DemonBehaviour : CharacterEntity
{
    private EnemyPool enemyPool;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyPool = transform.parent.GetComponent<EnemyPool>(); //gain access to the enemy pool when initailzed
    }

    // Update is called once per frame
    void Update()
    {
        if (activeHealth<=0f)
            Die();
    }

    public void Die()
    {
        enemyPool.DespawnEnemy(gameObject);
        activeHealth = definedHealth;
    }

}

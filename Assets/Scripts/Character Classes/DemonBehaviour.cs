using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class DemonBehaviour : CharacterEntity
{
    private EnemyPool enemyPool;
    private Transform playerTransform;
    public float chaseSpeed = 3f;
    BasicAttack basicAttack;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyPool = transform.parent.GetComponent<EnemyPool>(); //gain access to the enemy pool when initailzed
        playerTransform = PlayerManager.Player.transform; //gain access to player transform when initialized
        basicAttack = GetComponent<BasicAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf) //if demon is active and not in pool
            StartCoroutine(DetectPlayer());
        
        ChasePlayer();
        if (activeHealth<=0f)
            Die();

        basicAttack.Tick();
    }

    public void Die() //make demon "die" and reset health
    {
        enemyPool.DespawnEnemy(gameObject);
        activeHealth = definedHealth;
    }

    public void ChasePlayer()
    {
        Vector2 chaseDirection = (playerTransform.position - transform.position).normalized;
        rb.linearVelocity = chaseDirection * chaseSpeed;
    }

    IEnumerator DetectPlayer()
    {
        if (basicAttack.hasEnteredHitbox()) //if player has entered hitbox range, try attacking
        {
            yield return new WaitForSeconds(0.5f); //buffer of x seconds before try attacking, feels more realistic
            basicAttack.TryActivate();
        }
            
    }

}

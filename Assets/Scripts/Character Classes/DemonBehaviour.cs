using System;
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
        ChasePlayer();
        if (activeHealth<=0f)
            Die();
        DetectPlayer();

        basicAttack.Tick();
    }

    public void Die()
    {
        enemyPool.DespawnEnemy(gameObject);
        activeHealth = definedHealth;
    }

    public void ChasePlayer()
    {
        Vector2 chaseDirection = (playerTransform.position - transform.position).normalized;
        rb.linearVelocity = chaseDirection * chaseSpeed;
    }

    public void DetectPlayer()
    {
        float distance = Vector2.Distance(playerTransform.position,transform.position);
        if (distance <= 2f)
        {
            basicAttack.TryActivate();
        }
    }

}

using UnityEngine;
using System.Collections;

public class PlayerProperties: CharacterEntity
{

    void Update()
    {
        Die();
    }

    void Die()
    {
        if (activeHealth <= 0f)
        {
            Destroy(gameObject);
            print("You died!");
        }
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        HUDManager.Instance.UpdateHealth();
    }
}
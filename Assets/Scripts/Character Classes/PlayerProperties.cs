using UnityEngine;

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
}
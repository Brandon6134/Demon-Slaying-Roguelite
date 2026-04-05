using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;

public class Fireball : Projectile
{
    protected Vector2 direction;
    private float speed;
    private float projectileDuration;

    void Active()
    {
        rb.linearVelocity = speed * direction;
    }

    public void SetValues(float newDamage, float newSpeed, Vector2 newDirection, float newProjectileDuration)
    {
        damage = newDamage;
        speed = newSpeed;
        direction = newDirection;
        projectileDuration = newProjectileDuration;
        Active();
        StartCoroutine(DespawnAfterTime());
    }

    IEnumerator DespawnAfterTime()
    {
        yield return new WaitForSeconds(projectileDuration);
        ProjectileManager.Instance.pool.Despawn(gameObject);
    }
}

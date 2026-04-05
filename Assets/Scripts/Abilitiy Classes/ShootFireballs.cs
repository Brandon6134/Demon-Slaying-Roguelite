using UnityEngine;
using System.Collections;

public class ShootFireballs : Ability
{
    GameObject createdFireball;

    public float damage = 2f;
    protected Vector2 direction;
    public float speed = 2f;
    public Transform projectileTransform;
    public LayerMask enemyLayer;
    public float projectileDuration = 2f;
    
    public override void TryActivate()
    {
        if (!Ready()) return;

        createdFireball = ProjectileManager.Instance.pool.Spawn(projectileTransform.position);
        Fireball fireball = createdFireball.GetComponent<Fireball>();
        fireball.SetValues(damage,speed,player.transform.localScale.x * Vector3.right, projectileDuration); //control fireball damage, speed, direction, and duration here

        StartCooldown();
    }
}

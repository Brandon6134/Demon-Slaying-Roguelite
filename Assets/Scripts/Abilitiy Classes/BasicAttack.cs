using UnityEngine;

public class BasicAttack : Ability
{
    public Transform attackPos;
    public float attackRange;
    public float damage;
    public LayerMask enemyLayer;
    public AudioClip missSwordSlashSFX;
    public AudioClip hitSwordSlashSFX;
    public override void TryActivate()
    {
        if (!Ready()) return;

        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position,attackRange,enemyLayer);
        for (int i = 0; i < enemiesToDamage.Length; i++) //make all enemies in circle hitbox take damage
        {
            enemiesToDamage[i].GetComponent<DemonBehaviour>().TakeDamage(damage);
        }

        //plays correct sword slash sfx if enemy hit or missed
        if (enemiesToDamage.Length==0)
            AudioManager.Instance.PlaySFX(missSwordSlashSFX);
        else
            AudioManager.Instance.PlaySFX(hitSwordSlashSFX);

        StartCooldown();
    }

    void OnDrawGizmosSelected() //make circle hitbox visible in editor
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position,attackRange);
    }
}

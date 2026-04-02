using UnityEngine;

public class ChargeAttack : AttackSchema
{
    public AudioClip spinAttackSFX;
    public override void TryActivate()
    {
        if (!Ready()) return;

        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position,attackRange,enemyLayer);
        for (int i = 0; i < enemiesToDamage.Length; i++) //make all enemies in circle hitbox take damage
        {
            enemiesToDamage[i].GetComponent<DemonBehaviour>().TakeDamage(damage);
        }

        AudioManager.Instance.PlaySFX(spinAttackSFX,0.6f);

        StartCooldown();
    }

    void OnDrawGizmosSelected() //make circle hitbox visible in editor
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackPos.position,attackRange);
    }
}

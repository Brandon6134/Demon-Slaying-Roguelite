using UnityEngine;

public class BasicAttack : AttackSchema
{
    public AudioClip missSwordSlashSFX;
    public AudioClip hitSwordSlashSFX;

    public override void TryActivate()
    {
        if (!Ready()) return;

        if (animManager is PlayerAnimManager playerAnim)
        {
            playerAnim.SetIsBasicAttack(true);
        }

        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position,attackRange,enemyLayer);
        for (int i = 0; i < enemiesToDamage.Length; i++) //make all enemies in circle hitbox take damage
        {
            enemiesToDamage[i].GetComponent<CharacterEntity>().TakeDamage(damage);
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

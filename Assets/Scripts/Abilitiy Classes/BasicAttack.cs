using UnityEngine;

public class BasicAttack : AttackSchema
{
    public AudioClip missSwordSlashSFX;
    public AudioClip hitSwordSlashSFX;

    public override void TryActivate()
    {
        if (!Ready()) return;

        // if (animManager is PlayerAnimManager playerAnim)
        // {
        //     playerAnim.SetIsBasicAttack(true);
        // }
        // animManager.SetIsBasicAttack(false); //make sure is false before setting true

        // animManager.SetIsBasicAttack(true);
        animManager.TriggerBasicAttack();

        StartCooldown();
    }

    void BasicAttackHitbox() //to be called at the active sword hit animation frame
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position,attackRange,enemyLayer);
        for (int i = 0; i < enemiesToDamage.Length; i++) //make all enemies in circle hitbox take damage
        {
            enemiesToDamage[i].GetComponent<CharacterEntity>().TakeDamage(damage);
            enemiesToDamage[i].GetComponent<CharacterEntity>().TakeKnockback(knockbackDistance,transform,knockbackDuration,stunDuration);
        }

        //plays correct sword slash sfx if enemy hit or missed
        if (enemiesToDamage.Length==0)
            AudioManager.Instance.PlaySFX(missSwordSlashSFX);
        else
            AudioManager.Instance.PlaySFX(hitSwordSlashSFX);
    }

    void OnDrawGizmosSelected() //make circle hitbox visible in editor
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position,attackRange);
    }

    public bool hasEnteredHitbox() //returns true if enemy object is within hitbox range, false if not
    {
        return Physics2D.OverlapCircle(attackPos.position,attackRange,enemyLayer);
    }
}

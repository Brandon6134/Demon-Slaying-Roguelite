using UnityEngine;

public class ChargeAttack : AttackSchema
{
    public AudioClip spinAttackSFX;
    
    public float distanceFromPlayer;
    
    public override void TryActivate()
    {
        if (!Ready()) return;

        PlayerAnimManager.Instance.TriggerSpinAttack();

        AudioManager.Instance.PlaySFX(spinAttackSFX,0.6f);

        StartCooldown();
    }

    void Attack(Vector3 posModifier)
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position + posModifier*distanceFromPlayer,attackRange,enemyLayer);
        for (int i = 0; i < enemiesToDamage.Length; i++) //make all enemies in circle hitbox take damage
        {
            enemiesToDamage[i].GetComponent<CharacterEntity>().TakeDamage(damage);
            enemiesToDamage[i].GetComponent<CharacterEntity>().TakeKnockback(knockbackDistance,transform,knockbackDuration,stunDuration);
        }
    }
    
    //call the below hitboxes in animation frames
    void AttackBehind()
    {
        Attack(Vector3.left * player.transform.localScale.x);
    }

    void AttackAbove()
    {
        Attack(Vector3.up);
    }

    void AttackFront()
    {
        Attack(Vector3.right *player.transform.localScale.x);
    }

    void AttackBelow()
    {
        Attack(Vector3.down);
    }

    

    void OnDrawGizmosSelected() //make circle hitbox visible in editor
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackPos.position + Vector3.left*distanceFromPlayer,attackRange);
        Gizmos.DrawWireSphere(attackPos.position + Vector3.up*distanceFromPlayer,attackRange);
        Gizmos.DrawWireSphere(attackPos.position + Vector3.right*distanceFromPlayer,attackRange);
        Gizmos.DrawWireSphere(attackPos.position + Vector3.down*distanceFromPlayer,attackRange);
    }
}

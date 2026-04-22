using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
using System.Collections.Generic;

public class CharacterEntity : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //each character entity should have health, die when health = 0
    public float definedHealth = 10f;
    protected float activeHealth;
    protected bool isKnockedback = false;
    protected bool isStunned = false;
    public bool isInvulnerable {get; set;}
    public GameObject popupDamageText;
    protected Rigidbody2D rb;
    protected SpriteRenderer spriteRenderer {get;set;}
    private Pool dmgTextPool;
    private Color baseColor;

    void Awake()
    {
        activeHealth = definedHealth;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        dmgTextPool = GameObject.FindGameObjectWithTag("Damage Text Pool").GetComponent<Pool>();
        baseColor = spriteRenderer.color;
    }

    public virtual void TakeDamage(float damage)
    {
        if (isInvulnerable) return; //don't take damage if invulnerable

        activeHealth -= damage;
        Vector3 damageTextPos = gameObject.transform.position + new Vector3(Random.Range(-1f,1f),Random.Range(1f,2f),0);
        GameObject newPopupDamageText = dmgTextPool.Spawn(damageTextPos);
        newPopupDamageText.GetComponent<PopupDamageBehaviour>().SetText(damage);

        StartCoroutine(TurnRed());
    }

    public virtual void TakeKnockback(float knockbackDistance, Transform attackerTransform, float knockbackDuration, float stunDuration)
    {
        isKnockedback = true;
        Vector2 knockbackDirection = (transform.position - attackerTransform.position).normalized;
        rb.linearVelocity = knockbackDirection * knockbackDistance;
        StartCoroutine(KnockbackTimer(knockbackDuration,stunDuration));
    }

    IEnumerator KnockbackTimer(float knockbackDuration,float stunDuration)
    {
        yield return new WaitForSeconds(knockbackDuration);
        
        isStunned = true;
        rb.linearVelocity = Vector3.zero; //set stunned
        isKnockedback = false;
        
        StartCoroutine(StunTimer(stunDuration));
    }

    IEnumerator StunTimer(float stunDuration)
    {
        yield return new WaitForSeconds(stunDuration);
        isStunned = false;
    }

    IEnumerator TurnRed() //blink the entity red to indicate they've taken damage
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f); 
        spriteRenderer.color = baseColor;
    }

    public void ResetColor()
    {
        spriteRenderer.color = baseColor;
    }

    public float GetActiveHealth()
    {
        return activeHealth;
    }

    public float GetDefinedHealth()
    {
        return definedHealth;
    }

    public bool GetIsKnockedback()
    {
        return isKnockedback;
    }

    public bool GetIsStunned()
    {
        return isStunned;
    }

}

using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public LayerMask targetLayer;
    public float damage;
    public float knockbackDistance = 1f;
    public float knockbackDuration = 1f;
    public float stunDuration = 0.5f;
    protected Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if ((targetLayer & 1 << collision.gameObject.layer) != 0) //if object is on the targetLayer
        {
            collision.gameObject.GetComponent<CharacterEntity>().TakeDamage(damage);
            collision.gameObject.GetComponent<CharacterEntity>().TakeKnockback(knockbackDistance,transform,knockbackDuration,stunDuration);
        }
    }
}

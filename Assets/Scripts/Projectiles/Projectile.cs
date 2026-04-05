using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public LayerMask targetLayer;
    public float damage;
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
        }
    }
}

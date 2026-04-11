using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    public float cooldown = 0.5f;
    protected CharacterAnimManager animManager;
    protected float cooldownTimer;
    protected Rigidbody2D rb;
    protected PlayerController player;
    protected CharacterEntity entity;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<PlayerController>();
        animManager = GetComponent<CharacterAnimManager>();
        entity = GetComponent<CharacterEntity>();
    }

    public virtual void Tick()
    {
        cooldownTimer -= Time.deltaTime;
    }

    public bool Ready()
    {
        return cooldownTimer <= 0f;
    }

    protected void StartCooldown()
    {
        cooldownTimer = cooldown;
    }

    public abstract void TryActivate();
}

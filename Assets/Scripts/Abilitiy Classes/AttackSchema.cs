using UnityEngine;

public abstract class AttackSchema : Ability
{
    public Transform attackPos;
    public float attackRange;
    public float damage;
    public float knockbackDistance = 1f;
    public float knockbackDuration = 1f;
    public float stunDuration = 0.5f;
    //public Vector3 attackDisplacement;
    public LayerMask enemyLayer;
}

using UnityEngine;

public abstract class AttackSchema : Ability
{
    public Transform attackPos;
    public float attackRange;
    public float damage;
    //public Vector3 attackDisplacement;
    public LayerMask enemyLayer;
}

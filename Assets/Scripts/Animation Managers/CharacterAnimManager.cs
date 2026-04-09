using System;
using UnityEngine;

public abstract class CharacterAnimManager : MonoBehaviour
{
    protected Animator animator;
    protected String basicAttack = "isBasicAttack";
    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetIsBasicAttack(bool boolean)
    {
        animator.SetBool(basicAttack,boolean);
    }

    public void SetIsBasicAttackFalse() //called upon in event animation at end of basic attack animation
    {
        SetIsBasicAttack(false);
    }
}

using System;
using UnityEngine;

public abstract class CharacterAnimManager : MonoBehaviour
{
    protected Animator animator;
    protected String basicAttack = "BasicAttack";
    protected String direction = "direction";
    protected String directionX = "DirectionX";
    protected String directionY = "DirectionY";
    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void TriggerBasicAttack()
    {
        animator.SetTrigger(basicAttack);
    }

    public void SetDirection(Vector2 direction)
    {
        //animator.SetInteger(direction,value);
        animator.SetFloat(directionX,direction.x);
        animator.SetFloat(directionY,direction.y);
    }

}

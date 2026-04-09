using System;
using UnityEngine;

public class PlayerAnimManager : CharacterAnimManager
{
    public static PlayerAnimManager Instance;
    private String isWalking = "isWalking";

    private String isSpinAttack = "isSpinAttack";

    protected override void Awake()
    {
        base.Awake();
        Instance = this;
    }

    // public void SetIsBasicAttackFalse() //called upon in event animation at end of basic attack animation
    // {
    //     SetIsBasicAttack(false);
    // }

    // public void SetIsBasicAttack(bool boolean)
    // {
    //     animator.SetBool(basicAttack,boolean);
    // }

    public void SetIsWalkinkg(bool boolean)
    {
        animator.SetBool(isWalking,boolean);
    }

    public void SetIsSpinAttack(bool boolean)
    {
        animator.SetBool(isSpinAttack,boolean);
    }

    public void SetIsSpinAttackFalse()
    {
        SetIsSpinAttack(false);
    }
}

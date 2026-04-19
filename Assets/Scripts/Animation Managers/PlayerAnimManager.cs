using System;
using UnityEngine;

public class PlayerAnimManager : CharacterAnimManager
{
    public static PlayerAnimManager Instance;
    private String isWalking = "isWalking";

    private String spinAttack = "SpinAttack";

    protected override void Awake()
    {
        base.Awake();
        Instance = this;
    }

    public void SetIsWalkinkg(bool boolean)
    {
        animator.SetBool(isWalking,boolean);
    }

    public void TriggerSpinAttack()
    {
        animator.SetTrigger(spinAttack);
    }
}

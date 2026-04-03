using System;
using UnityEngine;

public class PlayerAnimManager : MonoBehaviour
{
    public static PlayerAnimManager Instance;
    private Animator animator;
    private String isWalking = "isWalking";
    private String basicAttack = "isBasicAttack";
    void Awake()
    {
        animator = GetComponent<Animator>();

        Instance = this;
    }

    public void SetIsBasicAttackFalse() //called upon in event animation at end of basic attack animation
    {
        SetIsBasicAttack(false);
    }

    public void SetIsBasicAttack(bool boolean)
    {
        animator.SetBool(basicAttack,boolean);
    }

    public void SetIsWalkinkg(bool boolean)
    {
        animator.SetBool(isWalking,boolean);
    }
}

using System;
using UnityEngine;

public class BasicDemonAnimManager : CharacterAnimManager
{
    public static BasicDemonAnimManager Instance;

    protected override void Awake()
    {
        base.Awake();
        Instance = this;
    }
}

using UnityEngine;

public class ProjectileManager : PoolManagerSchema
{
    public static ProjectileManager Instance;

    void Awake()
    {
        Instance = this;
    }
}

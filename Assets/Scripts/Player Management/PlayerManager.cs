using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static GameObject Player;

    void Awake()
    {
        Player = gameObject;
    }
}

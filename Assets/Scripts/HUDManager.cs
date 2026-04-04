using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance; //only one HUDManager class, so defining as Instance is fine
    public TextMeshProUGUI healthText;
    public CharacterEntity player;
    private String playerHealth;
    private String maxHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this; 
        maxHealth = player.GetDefinedHealth().ToString();
        UpdateHealth();
    }

    public void UpdateHealth()
    {
        playerHealth = player.GetActiveHealth().ToString();
        healthText.text = "HP: " + playerHealth + "/" + maxHealth;
    }
}

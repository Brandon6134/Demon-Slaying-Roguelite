using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance; //only one HUDManager class, so defining as Instance is fine
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI waveNumText;
    [SerializeField] TextMeshProUGUI objectiveText;
    [SerializeField] CharacterEntity player;
    [SerializeField] EnemyWaveManager enemyWaveManager;
    private string playerHealth;
    private string maxHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this; 
        maxHealth = player.GetDefinedHealth().ToString();
        IntializeText();
    }

    private void IntializeText()
    {
        UpdateHealth();
        UpdateWaveNumber();
        UpdateObjectiveText();
    }

    public void UpdateHealth()
    {
        playerHealth = player.GetActiveHealth().ToString();
        healthText.text = "HP: " + playerHealth + "/" + maxHealth;
    }

    public void UpdateWaveNumber()
    {
        waveNumText.text = "Wave: " + enemyWaveManager.GetWaveNumber();
    }

    public void UpdateObjectiveText()
    {
        objectiveText.text = "Kills: " + enemyWaveManager.GetCurrentKills() + "/" + enemyWaveManager.GetRequiredKills();
    }
}

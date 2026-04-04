using UnityEngine;

public class BloodCurrencyManager : MonoBehaviour
{
    public static BloodCurrencyManager Instance;
    private int bloodAmount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
    }

    public void AddBloodAmount(int addedBlood)
    {
        bloodAmount+=addedBlood;
    }

    public void SubtractBloodAmount(int subtractedBlood)
    {
        bloodAmount -= subtractedBlood;
    }

    public string GetBloodAmount()
    {
        return bloodAmount.ToString();
    }
}

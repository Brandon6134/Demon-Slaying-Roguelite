using TMPro;
using UnityEngine;

public class PopupDamageBehaviour : MonoBehaviour
{
    public TextMeshProUGUI damageText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject,1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        damageText.alpha -= Time.deltaTime;
    }

    public void SetText(float damage)
    {
        damageText.text = damage.ToString();
    }
}
